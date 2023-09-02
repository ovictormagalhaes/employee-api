
using System.Data;
using System.Reflection.Metadata;
using Dapper;

namespace Employee.API.Infrastructure.Repositories
{
    public interface IEmployeeRepository
    {
        Task CreateAsync(Domain.Employee employee);
        Task UpdateAsync(Domain.Employee employee);
        Task<Domain.Employee?> FirstOrDefaultByIdAsync(Guid id);
        Task<Domain.Employee?> FirstOrDefaultByDocumentAsync(string document);
        Task DeleteByIdAsync(Guid id);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _connection;

        public EmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateAsync(Domain.Employee employee)
        {
            const string query = @"INSERT INTO employees
                        (id, name, document, birthed_at, created_at)
                        VALUES
                        (@id, @name, @document, @birthedAt, @createdAt);";

            var parameters = new
            {
                id = employee.Id,
                name = employee.Name,
                document = employee.Document,
                birthedAt = employee.BirthedAt,
                createdAt = employee.CreatedAt
            };

            await _connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            const string query = "DELETE FROM employees WHERE id = @id";

            var parameters = new { id };

            await _connection.ExecuteAsync(query, parameters);
        }

        public async Task<Domain.Employee?> FirstOrDefaultByDocumentAsync(string document)
        {
            const string query = @"SELECT 
                        id,
                        name,
                        document, 
                        birthed_at as BirthedAt,
                        created_at as CreatedAt,
                        updated_at as UpdatedAt
                        FROM employees
                        WHERE document = @document;";

            var parameters = new { document };

            return await _connection.QueryFirstOrDefaultAsync<Domain.Employee?>(query, parameters);
        }

        public async Task<Domain.Employee?> FirstOrDefaultByIdAsync(Guid id)
        {
            const string query = @"SELECT 
                        id,
                        name,
                        document, 
                        birthed_at as BirthedAt,
                        created_at as CreatedAt,
                        updated_at as UpdatedAt
                        FROM employees
                        WHERE id = @id;";

            var parameters = new { id };

            return await _connection.QueryFirstOrDefaultAsync<Domain.Employee?>(query, parameters);
        }

        public async Task UpdateAsync(Domain.Employee employee)
        {
            var query = @"UPDATE employees SET 
                        name=@name,
                        document=@document,
                        birthed_at=@birthedAt,
                        updated_at=@updatedAt 
                        WHERE 
                        id=@id;";

            var parameters = new
            {
                id = employee.Id,
                name = employee.Name,
                document = employee.Document,
                birthedAt = employee.BirthedAt,
                updatedAt = employee.UpdatedAt
            };

            await _connection.ExecuteAsync(query, parameters);
        }
    }
}
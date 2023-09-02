using System.Data;
using Auth.API.Domain;
using Dapper;

namespace Auth.API.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User?> FirstOrDefaultByEmailAndPasswordAsync(string email, string password);
        Task<User?> FirstOrDefaultByEmailAsync(string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateAsync(User user)
        {
            const string query = @"INSERT INTO users
                        (id, name, email, password, created_at)
                        VALUES
                        (@id, @name, @email, @password, @createdAt);";

            var parameters = new
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                password = user.Password,
                createdAt = user.CreatedAt
            };

            await _connection.ExecuteAsync(query, parameters);
        }

        public Task<User?> FirstOrDefaultByEmailAndPasswordAsync(string email, string password)
        {
            const string query = @"SELECT id, name, email, password, created_at as CreatedAt
                        FROM users
                        WHERE email = @email and password = @password;";

            var parameters = new { email, password };

            return _connection.QueryFirstOrDefaultAsync<User?>(query, parameters);
        }

        public Task<User?> FirstOrDefaultByEmailAsync(string email)
        {
            const string query = @"SELECT id, name, email, password, created_at as CreatedAt
                        FROM users
                        WHERE email = @email;";

            var parameters = new { email };

            return _connection.QueryFirstOrDefaultAsync<User?>(query, parameters);
        }
    }
}
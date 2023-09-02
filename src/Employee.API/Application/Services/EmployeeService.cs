using Employee.API.Application.Inputs;
using Employee.API.Domain;
using Employee.API.Infrastructure.Repositories;

namespace Employee.API.Application.Services
{
    public interface IEmployeeService
    {
        Task<Domain.Employee> Create(CreateEmployeeInput input);
        Task<Domain.Employee?> Update(Guid id, UpdateEmployeeInput input);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Employee> Create(CreateEmployeeInput input)
        {
            var employee = new Domain.Employee(input.Name!, input.Document!, input.BirthedAt!.Value);

            await _employeeRepository.CreateAsync(employee);

            return employee;
        }

        public async Task<Domain.Employee?> Update(Guid id, UpdateEmployeeInput input)
        {
            var currentEmployee = await _employeeRepository.FirstOrDefaultByIdAsync(id);

            if (currentEmployee is null)
                return null;


            var employee = new Domain.Employee()
            {
                Id = id,
                Name = input.Name ?? currentEmployee.Name,
                Document = input.Document ?? currentEmployee.Document,
                BirthedAt = input.BirthedAt ?? currentEmployee.BirthedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = currentEmployee.CreatedAt
            };

            await _employeeRepository.UpdateAsync(employee);

            return employee;
        }
    }
}
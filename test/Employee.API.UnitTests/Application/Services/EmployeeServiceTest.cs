using Employee.API.Application.Services;
using Employee.API.Infrastructure.Repositories;
using Employee.API.UnitTests.Builders;
using FluentAssertions;
using Moq;

namespace Employee.API.UnitTests.Application.Services
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository = new(MockBehavior.Strict);


        [Fact]
        public async Task Create_ShouldReturnEmployee()
        {
            //Arrange
            var input = new CreateEmployeeInputBuilder().Generate();
            var service = new EmployeeService(_employeeRepository.Object);

            _employeeRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Employee>())).Returns(Task.CompletedTask);
            //Act
            var result = await service.Create(input);

            //Assert
            var expected = new Domain.Employee()
            {
                Name = input.Name,
                Document = input.Document,
                BirthedAt = input.BirthedAt.Value
            };

            result.Should().BeEquivalentTo(expected, config => config
                    .Excluding(x => x.Id)
                    .Excluding(x => x.CreatedAt)
                    .Excluding(x => x.UpdatedAt));
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenCannotFindEmployee()
        {
            //Arrange
            var id = Guid.NewGuid();
            var input = new UpdateEmployeeInputBuilder().Generate();
            var service = new EmployeeService(_employeeRepository.Object);

            _employeeRepository.Setup(x => x.FirstOrDefaultByIdAsync(id)).ReturnsAsync(null as Domain.Employee);
            //Act
            var result = await service.Update(id, input);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Update_ShouldReturnEmployee_WhenUpdateEmployee()
        {
            //Arrange
            var id = Guid.NewGuid();
            var input = new UpdateEmployeeInputBuilder().Generate();
            var service = new EmployeeService(_employeeRepository.Object);

            _employeeRepository.Setup(x => x.FirstOrDefaultByIdAsync(id)).ReturnsAsync(new Domain.Employee());
            _employeeRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Employee>())).Returns(Task.CompletedTask);
            //Act
            var result = await service.Update(id, input);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
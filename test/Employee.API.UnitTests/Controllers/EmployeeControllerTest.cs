using System.Net;
using Employee.API.Application.Inputs;
using Employee.API.Application.Services;
using Employee.API.Controllers;
using Employee.API.Infrastructure.Repositories;
using Employee.API.UnitTests.Builders;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Employee.API.UnitTests.Controllers
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeService> _employeeService = new(MockBehavior.Strict);
        private readonly Mock<IEmployeeRepository> _employeeRepository = new(MockBehavior.Strict);
        private readonly Mock<IValidator<CreateEmployeeInput>> _createEmployeeInputValidator = new(MockBehavior.Strict);
        private readonly Mock<IValidator<UpdateEmployeeInput>> _updateEmployeeInputValidator = new(MockBehavior.Strict);

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenInvalidInput()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var input = new CreateEmployeeInputBuilder().SetName(string.Empty).Generate();

            _createEmployeeInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { new ValidationFailure("Name", "cannot be empty") } });

            //Act
            var result = await controller.Create(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_ShouldReturnConflict_WhenAlreadyExistsEmployeeWithSameDocument()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var document = "123456789";
            var input = new CreateEmployeeInputBuilder().SetDocument(document).Generate();

            _createEmployeeInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _employeeRepository.Setup(x => x.FirstOrDefaultByDocumentAsync(document)).ReturnsAsync(new Domain.Employee());

            //Act
            var result = await controller.Create(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenCreateEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var input = new CreateEmployeeInputBuilder().Generate();

            _createEmployeeInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _employeeRepository.Setup(x => x.FirstOrDefaultByDocumentAsync(input.Document)).ReturnsAsync(null as Domain.Employee);
            _employeeService.Setup(x => x.Create(input)).ReturnsAsync(new Domain.Employee());

            //Act
            var result = await controller.Create(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task UpdateById_ShouldReturnBadRequest_WhenInvalidInput()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var input = new UpdateEmployeeInputBuilder().Generate();

            _updateEmployeeInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { new ValidationFailure("Name", "cannot be empty") } });

            //Act
            var result = await controller.UpdateById(Guid.NewGuid(), input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateById_ShouldReturnNotFound_WhenCannotFoundEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var id = Guid.NewGuid();
            var input = new UpdateEmployeeInputBuilder().Generate();

            _updateEmployeeInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _employeeService.Setup(x => x.Update(id, input)).ReturnsAsync(null as Domain.Employee);

            //Act
            var result = await controller.UpdateById(id, input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateById_ShouldReturnNotFound_WhenUpdateEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var id = Guid.NewGuid();
            var input = new UpdateEmployeeInputBuilder().Generate();

            _updateEmployeeInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _employeeService.Setup(x => x.Update(id, input)).ReturnsAsync(new Domain.Employee());

            //Act
            var result = await controller.UpdateById(id, input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenCannotFoundEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var id = Guid.NewGuid();

            _employeeRepository.Setup(x => x.FirstOrDefaultByIdAsync(id)).ReturnsAsync(null as Domain.Employee);

            //Act
            var result = await controller.GetById(id);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenFindEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var id = Guid.NewGuid();

            _employeeRepository.Setup(x => x.FirstOrDefaultByIdAsync(id)).ReturnsAsync(new Domain.Employee());

            //Act
            var result = await controller.GetById(id);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteById_ShouldReturnNotFound_WhenCannotFoundEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var id = Guid.NewGuid();

            _employeeRepository.Setup(x => x.FirstOrDefaultByIdAsync(id)).ReturnsAsync(null as Domain.Employee);

            //Act
            var result = await controller.DeleteById(id);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteById_ShouldReturnNoContent_WhenDeleteEmployee()
        {
            //Arrange
            var controller = new EmployeeController(_employeeService.Object, _employeeRepository.Object, _createEmployeeInputValidator.Object, _updateEmployeeInputValidator.Object);
            var id = Guid.NewGuid();

            _employeeRepository.Setup(x => x.FirstOrDefaultByIdAsync(id)).ReturnsAsync(new Domain.Employee());
            _employeeRepository.Setup(x => x.DeleteByIdAsync(id)).Returns(Task.CompletedTask);

            //Act
            var result = await controller.DeleteById(id);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.NoContent);
        }

        private static HttpStatusCode GetHttpStatusCode(IActionResult actionResult)
        {
            try
            {
                return (HttpStatusCode)actionResult
                    .GetType()
                    .GetProperty("StatusCode")
                    .GetValue(actionResult, null);
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
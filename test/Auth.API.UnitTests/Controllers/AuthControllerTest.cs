using System.Net;
using Auth.API.Application.Inputs;
using Auth.API.Application.Services;
using Auth.API.Controllers;
using Auth.API.Domain;
using Auth.API.Infrastructure.Repositories;
using Auth.API.Infrastructure.Securities;
using Auth.API.UnitTests.Builders;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Auth.API.UnitTests.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authService = new(MockBehavior.Strict);
        private readonly Mock<IUserRepository> _userRepository = new(MockBehavior.Strict);
        private readonly Mock<ITokenGenerator> _tokenGenerator = new(MockBehavior.Strict);
        private readonly Mock<IValidator<CreateUserInput>> _createUserInputValidator = new(MockBehavior.Strict);
        private readonly Mock<IValidator<LoginInput>> _loginInputValidator = new(MockBehavior.Strict);

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidInput()
        {
            //Arrange
            var controller = new AuthController(_authService.Object, _userRepository.Object,
                _tokenGenerator.Object, _createUserInputValidator.Object, _loginInputValidator.Object);
            var input = new CreateUserInputBuilder().SetName(string.Empty).Generate();

            _createUserInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { new ValidationFailure("Name", "cannot be empty") } });

            //Act
            var result = await controller.Register(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Register_ShouldReturnConflict_WhenAlreadyExistUser()
        {
            //Arrange
            var controller = new AuthController(_authService.Object, _userRepository.Object,
                _tokenGenerator.Object, _createUserInputValidator.Object, _loginInputValidator.Object);
            var input = new CreateUserInputBuilder().Generate();

            _createUserInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _userRepository.Setup(x => x.FirstOrDefaultByEmailAsync(input.Email!)).ReturnsAsync(new User());

            //Act
            var result = await controller.Register(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task Register_ShouldReturnCreated_WhenCreateUser()
        {
            //Arrange
            var controller = new AuthController(_authService.Object, _userRepository.Object,
                _tokenGenerator.Object, _createUserInputValidator.Object, _loginInputValidator.Object);
            var input = new CreateUserInputBuilder().Generate();

            _createUserInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _userRepository.Setup(x => x.FirstOrDefaultByEmailAsync(input.Email!)).ReturnsAsync(null as User);
            _authService.Setup(x => x.RegisterAsync(input)).ReturnsAsync(new User());

            //Act
            var result = await controller.Register(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenInvalidInput()
        {
            //Arrange
            var controller = new AuthController(_authService.Object, _userRepository.Object,
                _tokenGenerator.Object, _createUserInputValidator.Object, _loginInputValidator.Object);
            var input = new LoginInputBuilder().SetEmail(string.Empty).Generate();

            _loginInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { new ValidationFailure("Email", "cannot be empty") } });

            //Act
            var result = await controller.Login(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenInvalidEmailOrPassword()
        {
            //Arrange
            var controller = new AuthController(_authService.Object, _userRepository.Object,
                _tokenGenerator.Object, _createUserInputValidator.Object, _loginInputValidator.Object);
            var input = new LoginInputBuilder().Generate();

            _loginInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _authService.Setup(x => x.LoginAsync(input)).ReturnsAsync(null as User);

            //Act
            var result = await controller.Login(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCorrectEmailAndPassword()
        {
            //Arrange
            var controller = new AuthController(_authService.Object, _userRepository.Object,
                _tokenGenerator.Object, _createUserInputValidator.Object, _loginInputValidator.Object);
            var input = new LoginInputBuilder().Generate();
            var userId = Guid.NewGuid();
            var userName = "teste";

            _loginInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult() { Errors = { } });
            _authService.Setup(x => x.LoginAsync(input)).ReturnsAsync(new User() { Id = userId, Name = userName, Email = input.Email! });
            _tokenGenerator.Setup(x => x.Generate(userId.ToString(), userName, input.Email!)).Returns(Guid.NewGuid().ToString());

            //Act
            var result = await controller.Login(input);

            //Assert
            GetHttpStatusCode(result).Should().Be(HttpStatusCode.OK);
        }

        private static HttpStatusCode GetHttpStatusCode(IActionResult actionResult)
        {
            try
            {
                return (HttpStatusCode)actionResult
                    .GetType()
                    .GetProperty("StatusCode")
                    ?.GetValue(actionResult, null)!;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
using Auth.API.Application.Services;
using Auth.API.Domain;
using Auth.API.Infrastructure.Repositories;
using Auth.API.Infrastructure.Securities;
using Auth.API.UnitTests.Builders;
using FluentAssertions;
using Moq;

namespace Auth.API.UnitTests.Application.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository = new(MockBehavior.Strict);

        [Fact]
        public async void Login_ShouldReturnUser_WhenLoginIsValid()
        {
            //Arrange
            var input = new LoginInputBuilder().Generate();
            var service = new AuthService(_userRepository.Object);

            var passwordEncrypted = Cryptography.GenerateMD5(input.Password!);
            _userRepository.Setup(x => x.FirstOrDefaultByEmailAndPasswordAsync(input.Email!, passwordEncrypted))
                .ReturnsAsync(new User());

            //Act
            var result = await service.LoginAsync(input);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void Login_ShouldReturnNull_WhenLoginIsInvalid()
        {
            //Arrange
            var input = new LoginInputBuilder().Generate();
            var service = new AuthService(_userRepository.Object);

            var passwordEncrypted = Cryptography.GenerateMD5(input.Password!);
            _userRepository.Setup(x => x.FirstOrDefaultByEmailAndPasswordAsync(input.Email!, passwordEncrypted))
                .ReturnsAsync(null as User);

            //Act
            var result = await service.LoginAsync(input);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void Register_ShouldReturnUser_WhenValidFields()
        {
            //Arrange
            var input = new CreateUserInputBuilder().Generate();
            var service = new AuthService(_userRepository.Object);

            _userRepository.Setup(x => x
                .CreateAsync(It.Is<User>(x => x.Name == input.Name && x.Email == input.Email)))
                    .Returns(Task.CompletedTask);
            //Act
            var result = await service.RegisterAsync(input);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
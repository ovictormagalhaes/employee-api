using Auth.API.Infrastructure.Securities;
using FluentAssertions;

namespace Auth.API.UnitTests.Infrastructure.Securities
{
    public class TokenGeneratorTest
    {
        [Fact]
        public void GenerateMD5_ShouldReturnStringEmpty_WhenNotEmptyString()
        {
            //Arrange
            string text = string.Empty;

            //Act
            var encrypted = Cryptography.GenerateMD5(text);

            //Assert
            encrypted.Should().Be(string.Empty);
        }

        [Fact]
        public void GenerateMD5_ShouldReturnMD5_WhenNotEmptyString()
        {
            //Arrange
            string text = "12345";

            //Act
            var encrypted = Cryptography.GenerateMD5(text);

            //Assert
            encrypted.Should().Be("827ccb0eea8a706c4c34a16891f84e7b");
        }
    }
}
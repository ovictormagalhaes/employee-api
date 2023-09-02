using Auth.API.Application.Validators;
using Auth.API.UnitTests.Builders;
using FluentAssertions;

namespace Auth.API.UnitTests.Application.Validators
{
    public class LoginInputValidatorTest
    {
        [Fact]
        public void Validate_IsValidShouldReturnTrue_WhenEverthingHasBeenFileedInCorrely()
        {
            //Arrange
            var input = new LoginInputBuilder().Generate();
            var validator = new LoginInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenNameTooShort()
        {
            //Arrange
            var input = new LoginInputBuilder()
                .SetPassword("ABCD")
                .Generate();
            var validator = new LoginInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenEmailTooLong()
        {
            //Arrange
            const string tooLongString = "xgrdbmyttnymryraorsvwbvrkptpprrysmwrtiktndzypxvkcuahabphbjjgaidzccwuepwclmobmaeujxdvprcddlqsadsqfioyuymxvozwtveqhyutghumtfkdirdncwmscdupqxizsmohpeoxuyzoqbgymtaozaaqpnsrihcysleldahnncfabilndahnncfabiln";
            var input = new LoginInputBuilder()
                .SetEmail(tooLongString)
                .Generate();
            var validator = new LoginInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenInvalidEmail()
        {
            //Arrange
            var input = new LoginInputBuilder()
                .SetEmail("invalidemail.com")
                .Generate();
            var validator = new LoginInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenPasswordTooLong()
        {
            //Arrange
            const string tooLongString = "xgrdbmyttnymryraorsvwbvrkptpprrysmwrtiktndzypxvkcuahabphbjjgaidzccwuepwclmobmaeujxdvprcddlqsadsqfioyuymxvozwtveqhyutghumtfkdirdncwmscdupqxizsmohpeoxuyzoqbgymtaozaaqpnsrihcysleldahnncfabilndahnncfabiln";
            var input = new LoginInputBuilder()
                .SetPassword(tooLongString)
                .Generate();
            var validator = new LoginInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }
    }
}
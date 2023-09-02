using Auth.API.Application.Validators;
using Auth.API.UnitTests.Builders;
using FluentAssertions;

namespace Auth.API.UnitTests.Application.Validators
{
    public class CreateUserInputValidatorTest
    {
        [Fact]
        public void Validate_IsValidShouldReturnTrue_WhenEverthingHasBeenFileedInCorrely()
        {
            //Arrange
            var input = new CreateUserInputBuilder().Generate();
            var validator = new CreateUserInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenNameIsEmpty()
        {
            //Arrange
            var input = new CreateUserInputBuilder()
                .SetName(string.Empty)
                .Generate();
            var validator = new CreateUserInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenNameTooShort()
        {
            //Arrange
            var input = new CreateUserInputBuilder()
                .SetName("ABCD")
                .Generate();
            var validator = new CreateUserInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenPasswordTooShort()
        {
            //Arrange
            var input = new CreateUserInputBuilder()
                .SetPassword("ABCD")
                .Generate();
            var validator = new CreateUserInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }
        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenNameTooLong()
        {
            //Arrange
            const string tooLongString = "xgrdbmyttnymryraorsvwbvrkptpprrysmwrtiktndzypxvkcuahabphbjjgaidzccwuepwclmobmaeujxdvprcddlqsadsqfioyuymxvozwtveqhyutghumtfkdirdncwmscdupqxizsmohpeoxuyzoqbgymtaozaaqpnsrihcysleldahnncfabilndahnncfabiln";
            var input = new CreateUserInputBuilder()
                .SetName(tooLongString)
                .Generate();
            var validator = new CreateUserInputValidator();

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
            var input = new CreateUserInputBuilder()
                .SetEmail(tooLongString)
                .Generate();
            var validator = new CreateUserInputValidator();

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
            var input = new CreateUserInputBuilder()
                .SetEmail("invalidemail.com")
                .Generate();
            var validator = new CreateUserInputValidator();

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
            var input = new CreateUserInputBuilder()
                .SetPassword(tooLongString)
                .Generate();
            var validator = new CreateUserInputValidator();

            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }
    }
}
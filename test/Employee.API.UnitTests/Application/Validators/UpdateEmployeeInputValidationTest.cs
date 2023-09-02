using Employee.API.Application.Validators;
using Employee.API.UnitTests.Builders;
using FluentAssertions;

namespace Employee.API.UnitTests.Application.Validators
{
    public class UpdateEmployeeInputValidationTest
    {

        [Fact]
        public void Validate_IsValidShouldReturnTrue_WhenEverthingHasBeenFileedInCorrely()
        {
            //Arrange
            var input = new UpdateEmployeeInputBuilder().Generate();
            var validator = new UpdateEmployeeInputValidator();
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
            var input = new UpdateEmployeeInputBuilder()
                .SetName(string.Empty)
                .Generate();
            var validator = new UpdateEmployeeInputValidator();
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
            var input = new UpdateEmployeeInputBuilder()
                .SetName("ABCD")
                .Generate();
            var validator = new UpdateEmployeeInputValidator();
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
            var input = new UpdateEmployeeInputBuilder()
                .SetName(tooLongString)
                .Generate();
            var validator = new UpdateEmployeeInputValidator();
            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenDocumentIsEmpty()
        {
            //Arrange
            var input = new UpdateEmployeeInputBuilder()
                .SetDocument(string.Empty)
                .Generate();
            var validator = new UpdateEmployeeInputValidator();
            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenBirthedAtIsDateTimeMinValue()
        {
            //Arrange
            var input = new UpdateEmployeeInputBuilder()
                .SetBirthedAt(DateTime.MinValue)
                .Generate();
            var validator = new UpdateEmployeeInputValidator();
            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_IsValidShouldReturnFalse_WhenBirthedAtIsDateTimeGreaterThanUtcNow()
        {
            //Arrange
            var input = new UpdateEmployeeInputBuilder()
                .SetBirthedAt(DateTime.UtcNow.AddHours(1))
                .Generate();
            var validator = new UpdateEmployeeInputValidator();
            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }
    }
}
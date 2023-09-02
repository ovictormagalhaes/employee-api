using Employee.API.Application.Validators;
using Employee.API.UnitTests.Builders;
using FluentAssertions;

namespace Employee.API.UnitTests.Application.Validators
{
    public class CreateEmployeeInputValidationTest
    {

        [Fact]
        public void Validate_IsValidShouldReturnTrue_WhenEverthingHasBeenFileedInCorrely()
        {
            //Arrange
            var input = new CreateEmployeeInputBuilder().Generate();
            var validator = new CreateEmployeeInputValidator();
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
            var input = new CreateEmployeeInputBuilder()
                .SetName(string.Empty)
                .Generate();
            var validator = new CreateEmployeeInputValidator();
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
            var input = new CreateEmployeeInputBuilder()
                .SetName("ABCD")
                .Generate();
            var validator = new CreateEmployeeInputValidator();
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
            var input = new CreateEmployeeInputBuilder()
                .SetName(tooLongString)
                .Generate();
            var validator = new CreateEmployeeInputValidator();
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
            var input = new CreateEmployeeInputBuilder()
                .SetDocument(string.Empty)
                .Generate();
            var validator = new CreateEmployeeInputValidator();
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
            var input = new CreateEmployeeInputBuilder()
                .SetBirthedAt(DateTime.MinValue)
                .Generate();
            var validator = new CreateEmployeeInputValidator();
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
            var input = new CreateEmployeeInputBuilder()
                .SetBirthedAt(DateTime.UtcNow.AddHours(1))
                .Generate();
            var validator = new CreateEmployeeInputValidator();
            //Act
            var validate = validator.Validate(input);
            var isValid = validate.IsValid;

            //Assert
            isValid.Should().BeFalse();
        }
    }
}
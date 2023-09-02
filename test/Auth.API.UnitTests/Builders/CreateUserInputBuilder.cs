using Auth.API.Application.Inputs;
using AutoBogus;

namespace Auth.API.UnitTests.Builders
{
    public class CreateUserInputBuilder : AutoFaker<CreateUserInput>
    {
        public CreateUserInputBuilder()
        {
            RuleFor(x => x.Name, faker => faker.Random.String());
            RuleFor(x => x.Email, faker => faker.Internet.Email());
            RuleFor(x => x.Password, faker => faker.Random.String());
        }

        public CreateUserInputBuilder SetName(string name)
        {
            RuleFor(x => x.Name, faker => name);
            return this;
        }

        public CreateUserInputBuilder SetEmail(string email)
        {
            RuleFor(x => x.Email, faker => email);
            return this;
        }

        public CreateUserInputBuilder SetPassword(string password)
        {
            RuleFor(x => x.Password, faker => password);
            return this;
        }
    }
}
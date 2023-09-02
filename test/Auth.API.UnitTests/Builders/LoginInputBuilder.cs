using Auth.API.Application.Inputs;
using AutoBogus;

namespace Auth.API.UnitTests.Builders
{
    public class LoginInputBuilder : AutoFaker<LoginInput>
    {
        public LoginInputBuilder()
        {
            RuleFor(x => x.Email, faker => faker.Internet.Email());
            RuleFor(x => x.Password, faker => faker.Random.String());
        }

        public LoginInputBuilder SetEmail(string email)
        {
            RuleFor(x => x.Email, faker => email);
            return this;
        }

        public LoginInputBuilder SetPassword(string password)
        {
            RuleFor(x => x.Password, faker => password);
            return this;
        }
    }
}
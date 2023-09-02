using AutoBogus;
using Bogus;
using Employee.API.Application.Inputs;

namespace Employee.API.UnitTests.Builders
{
    public class CreateEmployeeInputBuilder : AutoFaker<CreateEmployeeInput>
    {
        public CreateEmployeeInputBuilder()
        {
            RuleFor(x => x.Name, faker => faker.Random.String());
            RuleFor(x => x.Document, faker => faker.Random.String());
        }

        public CreateEmployeeInputBuilder SetName(string name)
        {
            RuleFor(x => x.Name, faker => name);
            return this;
        }

        public CreateEmployeeInputBuilder SetDocument(string document)
        {
            RuleFor(x => x.Document, faker => document);
            return this;
        }

        public CreateEmployeeInputBuilder SetBirthedAt(DateTime birthedAt)
        {
            RuleFor(x => x.BirthedAt, faker => birthedAt);
            return this;
        }
    }
}
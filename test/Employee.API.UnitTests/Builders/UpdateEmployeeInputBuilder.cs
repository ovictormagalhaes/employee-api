using AutoBogus;
using Employee.API.Application.Inputs;

namespace Employee.API.UnitTests.Builders
{
    public class UpdateEmployeeInputBuilder : AutoFaker<UpdateEmployeeInput>
    {
        public UpdateEmployeeInputBuilder()
        {
            RuleFor(x => x.Name, faker => faker.Random.String());
            RuleFor(x => x.Document, faker => faker.Random.String());
        }

        public UpdateEmployeeInputBuilder SetName(string name)
        {
            RuleFor(x => x.Name, faker => name);
            return this;
        }

        public UpdateEmployeeInputBuilder SetDocument(string document)
        {
            RuleFor(x => x.Document, faker => document);
            return this;
        }

        public UpdateEmployeeInputBuilder SetBirthedAt(DateTime birthedAt)
        {
            RuleFor(x => x.BirthedAt, faker => birthedAt);
            return this;
        }
    }
}
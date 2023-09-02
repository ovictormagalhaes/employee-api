namespace Employee.API.Application.Inputs
{
    public record ReadEmployeeOutput(Guid Id, string Name, string Document, DateTime BirthedAt, DateTime CreatedAt, DateTime? UpdatedAt);
}
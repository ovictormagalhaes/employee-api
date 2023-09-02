namespace Employee.API.Application.Inputs
{
    public record CreateEmployeeOutput(Guid Id, string Name, string Document, DateTime BirthedAt, DateTime CreatedAt, DateTime? UpdatedAt);
}
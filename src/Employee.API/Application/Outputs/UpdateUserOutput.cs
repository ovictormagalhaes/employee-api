namespace Employee.API.Application.Inputs
{
    public record UpdateEmployeeOutput(Guid Id, string Name, string Document, DateTime BirthedAt, DateTime CreatedAt, DateTime? UpdatedAt);

}
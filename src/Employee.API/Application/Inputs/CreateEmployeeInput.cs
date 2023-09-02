namespace Employee.API.Application.Inputs
{
    public class CreateEmployeeInput
    {
        public string? Name { get; init; }
        public string? Document { get; init; }
        public DateTime? BirthedAt { get; init; }
    }
}
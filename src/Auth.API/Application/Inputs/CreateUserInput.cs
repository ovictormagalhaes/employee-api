namespace Auth.API.Application.Inputs
{
    public class CreateUserInput
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}
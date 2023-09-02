namespace Auth.API.Application.Inputs
{
    public class LoginInput
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}
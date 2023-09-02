namespace Auth.API.Application.Outputs
{
    public class LoginOutput
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }
        public DateTime? CreatedAt { get; init; }

        public LoginOutput(Guid id, string name, string email, string token, DateTime? createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Token = token;
            CreatedAt = createdAt;
        }
    }
}
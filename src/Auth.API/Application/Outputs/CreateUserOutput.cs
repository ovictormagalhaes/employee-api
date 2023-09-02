namespace Auth.API.Application.Outputs
{
    public class CreateUserOutput
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public DateTime? CreatedAt { get; init; }

        public CreateUserOutput(Guid id, string name, string email, DateTime? createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            CreatedAt = createdAt;
        }
    }
}
namespace Employee.API.Domain
{
    public class Employee
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Document { get; init; }
        public DateTime BirthedAt { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }

        public Employee() { }

        public Employee(string name, string document, DateTime birthedAt)
        {
            Id = Guid.NewGuid();
            Name = name;
            Document = document;
            BirthedAt = birthedAt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
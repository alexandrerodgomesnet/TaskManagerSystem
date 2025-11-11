namespace TaskManagerSystem.Core.Entities;

public class User(string name, string email)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
}
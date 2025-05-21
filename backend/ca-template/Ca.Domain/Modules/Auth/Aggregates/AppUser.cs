using Ca.Domain.Modules.Shared.Exceptions;

namespace Ca.Domain.Modules.Auth.Aggregates;

// Aggregate root
public class AppUser(
    string name,
    string email,
    bool isAlive
)
{
    public string Name { get; private set; } = name;
    public string Email { get; } = email;
    public string Password { get; private set; } = string.Empty;
    public bool IsAlive { get; private set; } = isAlive;

    public void ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new DomainException(nameof(newName), " cannot be null or empty");

        Name = newName;
    }

    public void ChangePassword(string newPassword) => Password = newPassword;
}
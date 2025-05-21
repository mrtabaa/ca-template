using Ca.Domain.Modules.Shared.Exceptions;

namespace Ca.Domain.Modules.Auth.Aggregates;

// Aggregate root
public class AppUser
{
    private AppUser()
    {
    }

    public AppUser(string name, string email, bool isAlive)
    {
        Name = name;
        Email = email;
        IsAlive = isAlive;
    }

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public bool IsAlive { get; private set; } = true;

    public static AppUser Create(string name, string email, string password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new DomainException("Name, Email and Password cannot be null or empty");

        return new AppUser
        {
            Name = name,
            Email = email,
            Password = password,
            IsAlive = true
        };
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new DomainException(nameof(newName), " cannot be null or empty");

        Name = newName;
    }

    public void ChangePassword(string newPassword) => Password = newPassword;
}
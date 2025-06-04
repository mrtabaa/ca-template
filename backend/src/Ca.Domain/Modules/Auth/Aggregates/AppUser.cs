using Ca.Domain.Modules.Auth.ValueObjects;
using Ca.Domain.Modules.Common.Exceptions;

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
        Email = Email.Create(email); // Use of ValueObject
        IsAlive = isAlive;
    }

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public string Password { get; private set; } = string.Empty;
    public bool IsAlive { get; private set; } = true;

    public static AppUser Create(string name, string emailRaw, string password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(nameof(name), "Name cannot be empty");

        var email = Email.Create(emailRaw); // ✅ value object handles validation

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new DomainException(nameof(password), "Password must be at least 8 characters");


        return new AppUser
        {
            Name = name,
            Email = email, // Use of ValueObject
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
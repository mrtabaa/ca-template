using Ca.Domain.Modules.Auth.ValueObjects;

namespace Ca.Domain.Modules.Auth.Aggregates;

// Aggregate root
public class AppUser
{
    // Used to create a new AppUser object
    private AppUser()
    {
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public UserName UserName { get; private set; }
    public Password? Password { get; private set; }

    public static AppUser Create(
        string firstNameRaw, string lastNameRaw, string emailRaw, string userNameRaw, string passwordRaw
    )
    {
        var firstName = FirstName.Create(firstNameRaw); // value object handles validation
        var lastName = LastName.Create(lastNameRaw);
        var email = Email.Create(emailRaw);
        var userName = UserName.Create(userNameRaw);
        var password = Password.Create(passwordRaw);

        return new AppUser
        {
            FirstName = firstName, // Use of ValueObject
            LastName = lastName,
            Email = email,
            UserName = userName,
            Password = password
        };
    }

    public static AppUser Rehydrate(string firstNameRaw, string lastNameRaw, string emailRaw, string userNameRaw) =>
        new()
        {
            FirstName = FirstName.Create(firstNameRaw),
            LastName = LastName.Create(lastNameRaw),
            Email = Email.Create(emailRaw),
            UserName = UserName.Create(emailRaw)
        };

    public void ChangeFirstName(string? newFirstName)
    {
        FirstName = FirstName.Create(newFirstName);
    }

    public void ChangeLastName(string? newLastName)
    {
        LastName = LastName.Create(newLastName);
    }

    public void ChangePassword(string newPasswordRaw) =>
        Password = Password.Create(newPasswordRaw);
}
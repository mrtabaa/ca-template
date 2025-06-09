using Ca.Domain.Modules.Auth.Enums;

namespace Ca.Domain.Modules.Auth.Entities;

public class AppPermission
{
    public string Code => Permission.ToString(); // Enum.ToString()
    public Permission Permission { get; init; }
    public string Description { get; init; } = string.Empty;
}
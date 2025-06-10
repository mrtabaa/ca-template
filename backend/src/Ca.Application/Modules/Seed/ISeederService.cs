namespace Ca.Application.Modules.Seed;

public interface ISeederService
{
    public Task SeedAppAdminAsync();
    public Task SeedRolesAndPermissionsAsync();
}
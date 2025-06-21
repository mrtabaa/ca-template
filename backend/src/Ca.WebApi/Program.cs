using Ca.Application.Modules.AccessControl;
using Ca.Application.Modules.AccessControl.Commands;
using Ca.Application.Modules.Auth;
using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Responses.AccessControl;
using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.AccessControl.Enums;
using Ca.Shared.Configurations.Common.SeedSettings;
using Ca.Shared.Results;
using Ca.WebApi;
using Ca.WebApi.Modules.Auth;
using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddWebApiServices(builder.Configuration);

#endregion

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Ca.WebApi"); }
    );

    // Seed SuperAdmin
    SuperAdminSeedInfo seedInfo = app.Services.GetRequiredService<IOptions<SuperAdminSeedInfo>>().Value;

    RegisterSuperAdminCommand command = AuthRequestMapper.MapRegisterSuperAdminRequestToRegisterCommand(seedInfo);

    using IServiceScope scope = app.Services.CreateScope();

    var accessControlService = scope.ServiceProvider.GetRequiredService<IAccessControlService>();
    var myCommand = new AccessRoleCommand(seedInfo.RoleName, [AccessPermissionType.AccessSuperAdminPanel.ToString()]);
    OperationResult<AccessRoleResponse> result =
        await accessControlService.SeedSuperAdminRoleAndPermissionsAsync(myCommand);
    Console.WriteLine(result.IsSuccess ? "SuperAdmin role created successfully." : "SuperAdmin role creation failed.");

    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    OperationResult<RegisterResponse> response = await authService.SeedSuperAdminAppUserAsync(command);
    Console.WriteLine(
        response.IsSuccess
            ? "SuperAdmin created successfully."
            : $"SuperAdmin creation failed with error {response.Error?.Message}."
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Ca.Application.Modules.Auth;
using Ca.Contracts.Responses.Auth;
using Ca.Shared.Results;
using Ca.WebApi;

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
    using IServiceScope scope = app.Services.CreateScope();
    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    OperationResult<RegisterResponse> result = await authService.SeedSuperAdminAppUserAsync();
    Console.WriteLine(
        result.IsSuccess
            ? "SuperAdmin created successfully."
            : $"SuperAdmin creation failed with error {result.Error?.Message}."
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
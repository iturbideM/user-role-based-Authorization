using DataAccess.Context;
using DataAccess.Repositories.SqlServer;
using DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using Security;
using Security.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContext, SecurityContext>();
builder.Services.AddScoped<IRepository<Credentials>, SqlServerRepository<Credentials>>();
builder.Services.AddScoped<IRepository<Role>, SqlServerRepository<Role>>();
builder.Services.AddScoped<IRepository<CredentialsRole>, SqlServerRepository<CredentialsRole>>();
builder.Services.AddScoped<IRepository<RolePermission>, SqlServerRepository<RolePermission>>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

{
    var testUser = new Credentials { Password = "admin", Email = "admin@admin.com" };
    var testUser2 = new Credentials { Password = "user", Email = "user@admin.com" };

    var testRole = new Role { Name = "Admin" };

    var testCredentialRole = new CredentialsRole() { UserId = 1, RoleId = 1 };
    var testCredentialRole2 = new CredentialsRole() { UserId = 2, RoleId = 1 };
    var testPermissionRole = new RolePermission() { RoleId = 1, Permission = Permission.ViewWeather };

    using var scope = app.Services.CreateScope();
    var credentialsRepo = scope.ServiceProvider.GetRequiredService<IRepository<Credentials>>();
    var roleRepo = scope.ServiceProvider.GetRequiredService<IRepository<Role>>();
    var CredentialRoleRepo = scope.ServiceProvider.GetRequiredService<IRepository<CredentialsRole>>();
    var RolePermissionRepo = scope.ServiceProvider.GetRequiredService<IRepository<RolePermission>>();
    credentialsRepo.Add(testUser);
    credentialsRepo.Add(testUser2);
    roleRepo.Add(testRole);
    CredentialRoleRepo.Add(testCredentialRole);
    CredentialRoleRepo.Add(testCredentialRole2);
    RolePermissionRepo.Add(testPermissionRole);
}

app.Run();

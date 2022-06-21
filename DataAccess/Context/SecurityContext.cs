using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Security.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Context
{
    [ExcludeFromCodeCoverage]
    public class SecurityContext : DbContext
    {
        public DbSet<Credentials>? Credentials { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<CredentialsRole>? CredentialsRoles { get; set; }
        public DbSet<RolePermission>? RolesPermissions { get; set; }
        
        public SecurityContext() { }
        public SecurityContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString(@"TickArenaDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
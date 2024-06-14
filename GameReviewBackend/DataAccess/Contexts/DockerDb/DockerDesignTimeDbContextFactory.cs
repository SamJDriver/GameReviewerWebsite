using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.DockerDb
{
    public class DockerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DockerDbContext>
    {
        // This method is called by the runtime.
        // Use the following commands to perform the following:
        // release: dotnet ef migrations script --idempotent --context DockerDbContext
        // update your local database with migrations: dotnet ef database update --context DockerDbContext
        // update your local database with seed data: dotnet ef database update --context DockerDbContext --seed
        // update your loca database with a generated script: dotnet ef migrations script --idempotent --context DockerDbContext

        public DockerDbContext CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "../API");
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddUserSecrets("34a2eb48-f55e-4322-8205-5f51e2572770")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DockerDb");

            var optionsBuilder = new DbContextOptionsBuilder<DockerDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            
            return new DockerDbContext(optionsBuilder.Options);
        }
    }
}
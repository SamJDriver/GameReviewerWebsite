using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.DockerDb
{
    public class DockerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DockerDbContext>
    {
        public DockerDbContext CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "../../../API");
            Console.WriteLine(path);

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../API"))
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
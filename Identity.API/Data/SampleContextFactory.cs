using Identity.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.API.Data;

public class SampleContextFactory : IDesignTimeDbContextFactory<IdentityDatabaseContext>
{
    public IdentityDatabaseContext CreateDbContext(string[] args)
    {
        IConfigurationRoot confuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();
        var build = WebApplication.CreateBuilder(args);
        var builder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
        var connectionString = confuration.GetValue<string>("Identity:ConnectionString");
        builder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Identity.API"));
        return new IdentityDatabaseContext(builder.Options);
    }
}
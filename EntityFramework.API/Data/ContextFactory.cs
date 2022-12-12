using EntityFramework.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFramework.API.Data;

public class ContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        IConfigurationRoot confuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();
        var build = WebApplication.CreateBuilder(args);
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        var connectionString = confuration.GetValue<string>("EntityFramework:ConnectionString");
        builder.UseSqlServer(connectionString, x => x.MigrationsAssembly("EntityFramework.API"));
        return new DatabaseContext(builder.Options);
    }
}
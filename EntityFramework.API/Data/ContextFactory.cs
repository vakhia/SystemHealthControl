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
            .AddJsonFile("appsettings.json")
            .Build();
        var build = WebApplication.CreateBuilder(args);
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        var connectionString = confuration.GetConnectionString("Connection");
        builder.UseSqlServer(connectionString, x => x.MigrationsAssembly("EntityFramework.API"));
        return new DatabaseContext(builder.Options);
    }
}
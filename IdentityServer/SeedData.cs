using System.Security.Claims;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<IdentityServerDatabaseContext>(
                options => options.UseSqlServer(connectionString)
            );

            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityServerDatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
            var dbContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            dbContext.Database.Migrate();
            EnsureSeedData(dbContext);
            var identityServerDatabaseContext = scope.ServiceProvider.GetService<IdentityServerDatabaseContext>();
            identityServerDatabaseContext.Database.Migrate();
            EnsureUsers(scope);
        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var migration = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var user = migration.FindByNameAsync("killerwhaleTest").Result;
            if (user == null)
            {
                user = new User()
                {
                    UserName = "killerwhaleTest",
                    SecondName = "killerwhale",
                    FirstName = "killerwhale",
                    Email = "killerwhaleTest++@gmail.com",
                    EmailConfirmed = true
                };
                var result = migration.CreateAsync(user, "Password@123").Result;
                result =
                    migration.AddClaimsAsync(
                        user,
                        new Claim[]
                        {
                            new Claim("role", "user")
                        }
                    ).Result;
            }

            var testAdmin = migration.FindByNameAsync("killerwhale-admin").Result;
            if (testAdmin == null)
            {
                testAdmin = new User()
                {
                    UserName = "killerwhale-admin",
                    FirstName = "killerwhale-admin",
                    SecondName = "killerwhale",
                    Email = "killerwhale-admin@gmail.com",
                    EmailConfirmed = true
                };
                var result = migration.CreateAsync(testAdmin, "Password@123").Result;

                result =
                    migration.AddClaimsAsync(
                        testAdmin,
                        new Claim[]
                        {
                            new Claim("role", "admin")
                        }
                    ).Result;
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetIdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.GetApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.GetApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }
}
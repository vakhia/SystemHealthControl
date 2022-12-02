using Identity.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL.Data;

public class IdentityDatabaseContext : IdentityDbContext<User>
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
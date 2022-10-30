using GraphQL.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        
    }

    public DbSet<Comment> Comments { get; set; }
}
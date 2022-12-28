using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultConnectionString = builder.Configuration.GetValue<string>("IdentityServer:ConnectionString");
builder.Services.AddDbContext<IdentityServerDatabaseContext>(options =>
    options.UseSqlServer(defaultConnectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServerDatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<User>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly("IdentityServer"));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly("IdentityServer"));
    })
    .AddDeveloperSigningCredential()
    .AddProfileService<ProfileService<User>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var seed = args.Contains("/seed");
if(seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

seed = true;
if (seed)
{
    SeedData.EnsureSeedData(defaultConnectionString);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseIdentityServer();

app.Run();
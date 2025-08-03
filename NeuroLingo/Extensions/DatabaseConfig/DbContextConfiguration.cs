using Microsoft.EntityFrameworkCore;
using NeuroLingo.Persistence.Data;

namespace NeuroLingo.Extensions.DatabaseConfig;

public static class DbContextConfiguration
{
    public static void SetupDatabase(this WebApplicationBuilder builder) 
    {
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found", nameof(connectionString));

        builder.Services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlite(connectionString));
    }
}

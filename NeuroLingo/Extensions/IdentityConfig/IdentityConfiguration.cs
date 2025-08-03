using Microsoft.AspNetCore.Identity;
using NeuroLingo.Features.Auth.Models;
using NeuroLingo.Persistence.Data;

namespace NeuroLingo.Extensions.IdentityConfig;

public static class IdentityConfiguration
{
    public static void SetupIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
        {
            // password settings
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            // user & sign-in settings
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
         .AddDefaultTokenProviders()
         .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}

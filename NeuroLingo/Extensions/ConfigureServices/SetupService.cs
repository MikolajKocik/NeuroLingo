using Microsoft.AspNetCore.Identity.UI.Services;
using NeuroLingo.Features.Auth.Services;
using NeuroLingo.Services.EmailNotifications;

namespace NeuroLingo.Extensions.ConfigureServices;

/// <summary>
/// Provides extension methods for configuring services in a <see cref="WebApplicationBuilder"/>.
/// </summary>
/// <remarks>This class contains methods to register application services with the dependency injection
/// container.</remarks>
public static class SetupService
{
    /// <summary>
    /// Configures the specified <see cref="WebApplicationBuilder"/> by registering application services.
    /// </summary>
    /// <remarks>This method adds the following services to the dependency injection container: <list
    /// type="bullet"> <item><description><see cref="IEmailSender"/> with a transient lifetime.</description></item>
    /// <item><description><see cref="IAuthService"/> with a scoped lifetime.</description></item> </list></remarks>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to configure.</param>
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IAuthService, AuthService>();
    }
}

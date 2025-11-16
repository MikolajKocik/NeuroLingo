using NeuroLingo.Utils.ValidationAttribute;

namespace NeuroLingo.Extensions.SetupCustomViews;

/// <summary>
/// Provides extension methods for configuring view-related options in a <see cref="WebApplicationBuilder"/>.
/// </summary>
/// <remarks>This class includes methods to customize MVC and Razor view configurations, such as adding global
/// filters and specifying custom view location formats.</remarks>
public static class ViewsOptions
{
    /// <summary>
    /// Configures custom services and options for the application, including MVC filters and Razor view locations.
    /// </summary>
    /// <remarks>This method adds a global filter for validating DTOs and customizes Razor view location
    /// formats  to search for views in specific directories under the "Core/Views" folder.</remarks>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure the application.</param>
    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidateDtoAttribute>();
        })
        .AddRazorOptions(options =>
        {
            // core
            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("Core/Views/Home/{0}.cshtml");
            options.ViewLocationFormats.Add("Core/Views/Shared/{0}.cshtml");
            options.ViewLocationFormats.Add("Core/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("{0}.cshtml");

            // partials
            options.ViewLocationFormats.Add("~/Pages/Shared/{0}.cshtml");
        });
    }
}

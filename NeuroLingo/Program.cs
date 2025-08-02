using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NeuroLingo.Extensions.DatabaseConfig;
using NeuroLingo.Extensions.IdentityConfig;
using NeuroLingo.Persistence.Data;
using NeuroLingo.Services.EmailNotifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("Core/Views/Home/{0}.cshtml");
        options.ViewLocationFormats.Add("Core/Views/Shared/{0}.cshtml");
        options.ViewLocationFormats.Add("Core/Views/{0}.cshtml");
    });

// Sqllite database context
DbContextConfiguration.SetupDatabase(builder);

// Identity service
IdentityConfiguration.SetupIdentity(builder.Services);

// SendGrid email service
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using NeuroLingo.Extensions.ConfigureServices;
using NeuroLingo.Extensions.DatabaseConfig;
using NeuroLingo.Extensions.IdentityConfig;
using NeuroLingo.Extensions.SetupCustomViews;

var builder = WebApplication.CreateBuilder(args);

// Custom Views
ViewsOptions.AddCustomConfiguration(builder);

// Sqllite database context
DbContextConfiguration.SetupDatabase(builder);

// Identity service
IdentityConfiguration.SetupIdentity(builder.Services);

// Add services
SetupService.Configure(builder);

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

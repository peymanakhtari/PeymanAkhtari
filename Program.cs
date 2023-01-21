using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PeymanAkhtari.Data;
using PeymanAkhtari.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
             .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
             .AddDataAnnotationsLocalization();

builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    //options.UseSqlServer(config.GetConnectionString("local"));
    //options.UseSqlServer(config.GetConnectionString("remote"));
    options.UseSqlServer(config.GetConnectionString("remote2"));
    //options.UseSqlServer(config.GetConnectionString("server2"));
    //options.UseSqlServer(config.GetConnectionString("server"));
    //  options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddAuthentication()
    .AddCookie("AdminAuth", x =>
{
    x.LoginPath = "/Admin/Login";
})
            .AddGoogle(options =>
            {
                options.ClientId = "754233785357-k8jk9kkr1m9se84idhfkcr88bggrn264.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});
builder.Services.AddDataProtection()
              .SetApplicationName("my-app")
              .PersistKeysToFileSystem(new DirectoryInfo(@"wwwroot\MyWebSite-keys"));

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Home/Error");

    //app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("en"),
                new CultureInfo("fa")
            };
var options = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
};
app.UseRequestLocalization(options);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

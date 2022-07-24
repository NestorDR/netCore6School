// The typical pattern is to call methods in the following order:
//      builder.Services.Add{Service}
//      builder.Services.Configure{Service}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmailService;
using SchoolWeb.Data;
using SchoolWeb.Factory;
using SchoolWeb.Models;

var builder = WebApplication.CreateBuilder(args);

# region Add services to the DI Container.

/*
  From here start the added of services to the dependency injection container.
  Previous to ASP .Net Core 6 these services were added in ConfigureServices method of Startup.cs.
 
  By using the DI pattern, the controller or Razor Page:
    - Does not use concrete types, only the interfaces it implements;
      that makes it easy to change the implementation without modifying the controller or Razor Page.
    - Does not create an instance of the concrete type, it is created by the DI container.
*/

// Add MVC service extension to support MVC app using controllers with views, but not razor pages.
// AddControllersWithViews method covers MVC, and AddMvc method covers both MVC and Razor Pages. I never would have guessed that. 
builder.Services.AddControllersWithViews();

// Add DB context service that tells the app that it has to use the DbContext which has inside AppDbContext,
//   and over a SQL Server set using the connection string defined in .\appsettings.json
builder.Services.AddDbContext<AppDbContext>(
    // Method GetConnectionString only look inside de block "ConnectionStrings" of .\appsettings.json
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Identity Framework Services are made available to the app through dependency injection.
// User class extends to IdentityUser class. Previously AddIdentity<IdentityUser, IdentityRole>()
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        // Identity Framework settings

        // Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
        options.Password.RequiredUniqueChars = 0;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 1000;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;

    })
    .AddEntityFrameworkStores<AppDbContext>()   // Add identity information stores.
    .AddDefaultTokenProviders();                // Add token provider to enable the token generation used for reset passwords.

// Set for password reset tokens a limited time of validity, for example, 2 hours.
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

// Register class CustomClaimsFactory to implement additional custom Claims.
// (The application registers the interface service with the concrete type.
//  The AddScoped method registers the service with a scoped lifetime to a single request.)
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

// AutoMapper is a simple library that helps us to transform one object type to another.
// It is a convention-based object-to-object mapper that requires very little configuration. 
// Visit: https://code-maze.com/automapper-net-core/
builder.Services.AddAutoMapper(typeof(Program));

// Extract email configuration values from the .\appsettings.json file and register EmailConfiguration as a singleton
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

// Register class EmailSender to create and send an email message.
// (The application registers the interface service with the concrete type.
//  The AddScoped method registers the service with a scoped lifetime to a single request.)
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Configure/register a cookie authentication scheme for the application
// The default route where ASP.NET Core Identity will look for Login action is on the /Account/Login.
// But for a different route, for example: /Identity/Account/Login, you have to configure app cookie setting LoginPath = "/Identity/Account/Login"
// About good security practices, visit: https://brokul.dev/authentication-cookie-lifetime-and-sliding-expiration
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

#endregion 


# region Configure the HTTP request pipeline with the necessary Middleware

// ASP .Net Core create an HTTP App Pipeline that process the request.
// From here start the middleware pipeline configuration. Pipeline that all the request goes through before reaching the server.
// Previous to ASP .Net 6 Core this HTTP Pipeline was configured in Configure method of Startup.cs.

// Here is read the environment var "ASPNETCORE_ENVIRONMENT" in .\properties\launchSettings.json
if (!app.Environment.IsDevelopment())   
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enforce redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable static files to be served from wwwroot folder, but...
app.UseStaticFiles();
// ... to use another static folder
// app.UseStaticFiles(new StaticFileOptions() { 
//     FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFolder")),
//     RequestPath = "/MyStaticFolder"
// });


// Add/Enable route matching to the middleware pipeline.
// This middleware looks at the set of endpoints defined in the app, and selects the best match based on the request.
app.UseRouting();

// Authenticate the user before they're allowed access to secure resources.
app.UseAuthentication();

// Authorize a user to access secure resources.
app.UseAuthorization();

// Set default route when areas are used, but controller o action method aren't specified in the URL
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Set default route, used if controller o action method aren't specified in the URL
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

#endregion

app.Run();

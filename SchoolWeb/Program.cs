using SchoolWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

# region Add services to the DI Container.

// From here start the added of services to the dependency injection container
// Previous to ASP .Net Core 6 this services was added in ConfigureServices method of Startup.cs.

// Add MVC service extension to support MVC app using controllers with views, but not razor pages
builder.Services.AddControllersWithViews();

// Add DB context service that tells the app that it has to use the DbContext which has inside AppDbContext,
//   and over a SQL Server set using the connection string defined in .\appsettings.json
builder.Services.AddDbContext<AppDbContext>(
    // Method GetConnectionString only look inside de block "ConnectionStrings" of appsettings.json
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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

// Enable static files to be served.
app.UseStaticFiles();

// Add/Enable route matching to the middleware pipeline.
// This middleware looks at the set of endpoints defined in the app, and selects the best match based on the request.
app.UseRouting();

// Authenticate the user before they're allowed access to secure resources.
// app.UseAuthentication();         // Here would be the auth, if it were implemented

// Authorize a user to access secure resources.
app.UseAuthorization();

// Set default route, used if controller o action method aren't specified in the URL
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

#endregion

app.Run();

using SchoolWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

# region Add services to the dependency injection container.

// Add MVC service extension to support MVC app using controllers with views, but not razor pages
builder.Services.AddControllersWithViews();

// Add DB context service that tells the app that it has to use the DbContext which has inside AppDbContext,
//   and over a SQL Server set using the connection string defined in .\appsettings.json
builder.Services.AddDbContext<AppDbContext>(
    // Method GetConnectionString only look inside de block "ConnectionStrings" of appsettings.json
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

#endregion

var app = builder.Build();

// From here start the middleware pipeline that the request goes through before reaching the server
# region Middleware Pipeline

// Configure the HTTP request pipeline.
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

// Add route matching to the middleware pipeline.
// This middleware looks at the set of endpoints defined in the app, and selects the best match based on the request.
app.UseRouting();

// Authenticate the user before they're allowed access to secure resources.
// app.UseAuthentication();         // Here would be the auth, if it were implemented

// Authorize a user to access secure resources.
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");     // Default route if controller o action method aren't specified in the URL

#endregion

app.Run();

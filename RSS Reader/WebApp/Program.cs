
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Adding Authentication Services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    }
);

// Interface and service need to be Added to Services and scoped to One per client request (connection)
// aka I need to tell it to create the service once per request
// https://stackoverflow.com/questions/46930090/unable-to-resolve-service-for-type-while-attempting-to-activate
builder.Services.AddScoped<FeedService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable directory browsing on the current path
// app.UseDirectoryBrowser();

app.UseRouting();

// Tells app to use Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

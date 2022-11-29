using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Study_Tracker.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Study_TrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Study_TrackerContext") ?? throw new InvalidOperationException("Connection string 'Study_TrackerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Accounts/Login";
        option.ExpireTimeSpan= TimeSpan.FromMinutes(10);
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Register}/{id?}");

app.Run();

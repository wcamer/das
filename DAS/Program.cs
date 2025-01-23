using DAS.Components;
using Microsoft.EntityFrameworkCore;
using DAS.Data;
using DAS;
using Microsoft.AspNetCore.Identity;
using DAS.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<DASContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DASContext")));
builder.Services.AddDbContextFactory<DASContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DASContext") ?? throw new InvalidOperationException("Connection string 'DASContext' not found.")));

builder.Services.AddScoped<AppState>();

//This is for password hashing
builder.Services.AddScoped<IPasswordHasher<Profile>, PasswordHasher<Profile>>();

//Adds the UserService to be discovered globally
builder.Services.AddScoped<UserService>();
//Add the AuthService to be discovered globally
builder.Services.AddScoped<AuthService>();
//Add a persistant storage
builder.Services.AddScoped<ProtectedLocalStorage>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//Authentication service using cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "patient_token";
        options.LoginPath = "/patientLogin";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthentication();
//This will pass the authentication state throughout the app
builder.Services.AddCascadingAuthenticationState();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using DAS.Components;
using Microsoft.EntityFrameworkCore;
using DAS.Data;
using DAS;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<DASContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DASContext")));
builder.Services.AddDbContextFactory<DASContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DASContext") ?? throw new InvalidOperationException("Connection string 'DASContext' not found.")));

builder.Services.AddScoped<AppState>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

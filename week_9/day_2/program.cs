using ContactCachingAPI.Repositories;
using ContactCachingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register IMemoryCache
builder.Services.AddMemoryCache();

// Register our classes so Dependency Injection works
builder.Services.AddSingleton<ContactRepository>();
builder.Services.AddScoped<ContactService>();

var app = builder.Build();

app.MapControllers();
app.Run();

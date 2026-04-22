using ContactPagingAPI.Data;
using ContactPagingAPI.Repositories;
using ContactPagingAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connect to SQL Server (update your connection string in appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ContactRepository>();
builder.Services.AddScoped<ContactService>();

var app = builder.Build();

app.MapControllers();
app.Run();

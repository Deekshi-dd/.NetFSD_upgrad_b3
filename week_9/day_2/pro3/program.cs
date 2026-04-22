using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 5;                        // Max 5 requests
        limiterOptions.Window = TimeSpan.FromSeconds(60);      // Per 60 seconds
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 0;                         // No queuing
    });

    // Return custom message when limit is exceeded
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync(
            "Too many requests. Please try again later.", token);
    };
});

var app = builder.Build();

app.UseRateLimiter();   // Must be added before MapControllers

app.MapControllers();
app.Run();

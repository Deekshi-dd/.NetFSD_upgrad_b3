using ContactManagementApi.Interfaces;
using ContactManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Dependency Injection
builder.Services.AddSingleton<IContactService, ContactService>();

var app = builder.Build();

// Developer Exception Page
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();

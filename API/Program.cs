using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.


builder.Services.AddScoped<SqliteConnection>(provider =>
{
    // luodaan connection instanssi niin kuin konstruktorissakin
    var connection = new SqliteConnection("Data Source=tuntiharjoitus2.db");
    // avataan ja palautetaan se
    connection.Open();
    return connection;
});

builder.Services.AddScoped<IAuthRepo, AuthSQLiteRepository>();
builder.Services.AddScoped<ILogRepo, LogSQLiteRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

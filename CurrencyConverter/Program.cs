using CurrencyConverter.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton<IFrankFurterService, FrankfurterService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using CEDEARsTracker.Infraestructure;
using CEDEARsTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpClient<IMarketClientService, MarketClientService>();


Console.WriteLine("--> Using SQL Server Db");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

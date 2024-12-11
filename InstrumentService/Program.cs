using InstrumentService.Infraestructure.Repositories;
using InstrumentService.Infraestructure.Repositories.Interfaces;
using InstrumentService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using InstrumentService.Data;
using InstrumentService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository>();
builder.Services.AddScoped<IInstrumentService, InstrumentService.Services.InstrumentService>();
builder.Services.AddHttpClient<IHttpMovementDataClient, HttpMovementDataClient>();

// Configure DbContext based on the environment
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));
}
else
{
    // Console.WriteLine("--> Using SQL Server Db");
    // builder.Services.AddDbContext<AppDbContext>(opt =>
    //     opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// PrepDb

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


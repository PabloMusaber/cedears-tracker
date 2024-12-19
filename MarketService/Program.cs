using MarketService.AsyncDataServices;
using MarketService.Data;
using MarketService.EventProcessing;
using MarketService.Infraestructure.Repositories;
using MarketService.Infraestructure.Repositories.Interfaces;
using MarketService.Services;
using MarketService.Services.Interfaces;
using MarketService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IInstrumentBalanceRepository, InstrumentBalanceRepository>();
builder.Services.AddScoped<IInstrumentBalanceService, InstrumentBalanceService>();
builder.Services.AddScoped<IInstrumentBalanceDataClient, InstrumentBalanceDataClient>();
builder.Services.AddHttpClient<IMarketClientService, MarketClientService>();
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

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

PrepDb.PrepPopulation(app);

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

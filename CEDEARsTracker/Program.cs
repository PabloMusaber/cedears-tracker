using CEDEARsTracker.Configuration;
using CEDEARsTracker.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiDocumentation();
builder.Services.AddHttpClients();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCustomUtilities();
builder.Services.AddControllersWithFeatures();
builder.Services.AddDataServices(builder.Configuration);

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

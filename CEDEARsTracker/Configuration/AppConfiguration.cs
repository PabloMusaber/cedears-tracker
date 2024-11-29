using CEDEARsTracker.Infraestructure;
using CEDEARsTracker.Infraestructure.Repositories;
using CEDEARsTracker.Infraestructure.Repositories.Interfaces;
using CEDEARsTracker.Services;
using CEDEARsTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CEDEARsTracker.Configuration
{
    public static class AppConfiguration
    {
        public static void AddApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IMarketClientService, MarketClientService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IInstrumentRepository, InstrumentRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMovementService, MovementService>();
        }

        public static void AddCustomUtilities(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddControllersWithFeatures(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("AppConnection")));
        }
    }
}
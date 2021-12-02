using Fleet.data;
using Fleet.data.Interfaces;
using Fleet.data.Repositories;
using Fleet.service;
using Fleet.service.Main;
using Fleet.service.Third_Party;
using Fleet.uow;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Fleet.util;

public static class IocRegistrations
{
    public static IServiceCollection AddAppSettingsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        //Repositories
        services.AddScoped<IFleetRepository, FleetRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleLogItemRepository, VehicleLogItemRepository>();

        //Services
        services.AddScoped<IFleetService, FleetService>();
        services.AddScoped<IVehicleService, VehicleService>();
        
        //UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Helpers
        services.AddScoped<IUploadService, UploadService>();
        services.AddScoped<ICsvParser, CsvParser>();
        return services;
    }
    
}
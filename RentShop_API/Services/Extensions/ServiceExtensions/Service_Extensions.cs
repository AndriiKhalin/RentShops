using Interfaces.IEntityService;
using Interfaces.IRepository;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services.Service.EntityService;

namespace Services.Extensions.ServiceExtensions;

public static class Service_Extensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
    }
}
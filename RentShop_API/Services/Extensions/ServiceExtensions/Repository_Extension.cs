using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services.Interfaces.IRepository;

namespace Services.Extensions.ServiceExtensions;

public static class Repository_Extension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
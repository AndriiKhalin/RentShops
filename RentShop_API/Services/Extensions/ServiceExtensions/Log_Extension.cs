using Interfaces.ILoggerService;
using Microsoft.Extensions.DependencyInjection;
using Services.LoggerService;

namespace Services.Extensions.ServiceExtensions;

public static class Log_Extension
{
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
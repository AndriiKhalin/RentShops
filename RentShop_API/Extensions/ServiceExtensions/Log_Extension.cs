using Interfaces.ILoggerService;
using LoggerService;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.ServiceExtensions;

public static class Log_Extension
{
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
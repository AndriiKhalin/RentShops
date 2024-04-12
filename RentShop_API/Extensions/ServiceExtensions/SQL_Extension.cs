using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Extensions.ServiceExtensions;

public static class SQL_Extension
{
    public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["ConnectionStrings:AppDb"];
        services.AddDbContext<RentDbContext>(o => o.UseSqlServer(connectionString));
    }
}
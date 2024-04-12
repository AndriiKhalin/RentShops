using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Models.SeedData;

public static class TasksInitializer
{
    public static WebApplication Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {

            using var context = scope.ServiceProvider.GetRequiredService<RentDbContext>();

            SeedData.SeedDates(context);

        }
        return app;
    }
}
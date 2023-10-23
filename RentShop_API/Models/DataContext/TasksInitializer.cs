using RentShop.Models;
using System;

namespace RentShop_API.Models.Data;

public static class TasksInitializer
{
    public static WebApplication Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {


            using var context = scope.ServiceProvider.GetRequiredService<RentDbContext>();

            SeedData.SeedBikes(context);
            SeedData.SeedScooters(context);
            SeedData.SeedStatus(context);
            SeedData.SeedClients(context);
        }
        return app;
    }
}
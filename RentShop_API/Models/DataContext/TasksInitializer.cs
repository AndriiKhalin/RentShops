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

            SeedData.SeedUsers(context);
            SeedData.SeedOrders(context);
            SeedData.SeedTransports(context);
            SeedData.SeedTransportAvailable(context);
            SeedData.SeedShops(context);
            SeedData.SeedCategories(context);
            SeedData.SeedLogTransactions(context);
            SeedData.SeedRating(context);
            SeedData.SeedTransactions(context);
        }
        return app;
    }
}
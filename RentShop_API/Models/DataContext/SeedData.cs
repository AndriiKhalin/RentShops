using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;


using System;
using RentShop_API.Models.Data;
using RentShop_API.Models.Entities;

namespace RentShop.Models;

public static class SeedData
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {

    }

    public static void SeedOrders(this ModelBuilder modelBuilder)
    {
    }

    public static void SeedTransports(this ModelBuilder modelBuilder)
    {

    }

    public static void SeedShops(this ModelBuilder modelBuilder)
    {

    }

    public static void SeedCategories(this ModelBuilder modelBuilder)
    {

    }

    public static void SeedTransactions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 0,
                Date = DateTime.Now,
                OrderId = Guid.Empty,
                Order = new Order(),
                LogTransaction = new LogTransaction()
            });
    }

    public static void SeedLogTransactions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogTransaction>().HasData(
            new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = true,
                TransactionId = Guid.Empty,
                Transaction = new Transaction()
            });

    }

    public static void SeedTransportAvailable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransportAvailable>().HasData(
            new TransportAvailable
            {
                Id = Guid.NewGuid(),    
                CountTransport = 0,
                TransportId = Guid.Empty,
                Transport = new Transport(),
                ShopId = Guid.Empty,  
                Shop = new Shop(),
            }
            );
    }
    public static void SeedRating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rating>().HasData(
            new Rating
            {
                Id = Guid.NewGuid(),
                Grand = "",
                Comment = "",
                CreatedAt = DateTime.UtcNow,
                UserId = Guid.Empty,
                User = new User(),
                TransportsId = Guid.Empty,
                Transport = new Transport()

            });
    }
}
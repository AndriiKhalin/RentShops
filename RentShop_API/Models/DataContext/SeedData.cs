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
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Andrew",
                LastName = "Khalin",
                Email = "khalin2002@gmail.com",
                Password = "10122002",
                Phone = "+380737303288",
                BirthDate = new DateTime(2002, 12, 10),
                Orders = new List<Order>() { },
                Ratings = new List<Rating>(),
                Role = "User"
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Vanya",
                LastName = "Lebid",
                Email = "lebid@gmail.com",
                Password = "1920202",
                Phone = "+380737303277",
                BirthDate = new DateTime(2001, 10, 5),
                Orders = new List<Order>() { },
                Ratings = new List<Rating>(),
                Role = "User"
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Vlad",
                LastName = "Shabaltas",
                Email = "shabaltas@gmail.com",
                Password = "648577",
                Phone = "+380991833277",
                BirthDate = new DateTime(2004, 2, 18),
                Orders = new List<Order>() { },
                Ratings = new List<Rating>(),
                Role = "Admin"
            }
        );
    }

    public static void SeedOrders(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = Guid.NewGuid(),
                DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 55, 03),
                DateTo = DateTime.Now,
                Price = 55f,
                Shops = new List<Shop>(),
                Transports = new List<Transport>(),
                UserId = null

            },
            new Order
            {
                Id = Guid.NewGuid(),
                DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 33, 13),
                DateTo = DateTime.Now,
                Price = 35f,
                Shops = new List<Shop>(),
                Transports = new List<Transport>(),
                UserId = null

            },
            new Order
            {
                Id = Guid.NewGuid(),
                DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 25, 53),
                DateTo = DateTime.Now,
                Price = 155f,
                Shops = new List<Shop>(),
                Transports = new List<Transport>(),
                UserId = null

            }
        );
    }

    public static void SeedTransports(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transport>().HasData(
            new Transport()
            {
                Id = Guid.NewGuid(),
                Mark = "Honda",
                Model = "V3",
                PriceMinute = 3.5f,
                ImgUrl = "http://...",
                MaxSpeed = 35,
                MaxWeight = 125,
                Ratings = new List<Rating>(),

                Orders = new List<Order>(),

                CategoryId = null
            },
            new Transport()
            {
                Id = Guid.NewGuid(),
                Mark = "Tesla",
                Model = "Skod",
                PriceMinute = 4.5f,
                ImgUrl = "http://...",
                MaxSpeed = 45,
                MaxWeight = 115,
                Ratings = new List<Rating>(),

                Orders = new List<Order>(),

                CategoryId = null
            },
            new Transport()
            {
                Id = Guid.NewGuid(),
                Mark = "Volva",
                Model = "Speed",
                PriceMinute = 3f,
                ImgUrl = "http://...",
                MaxSpeed = 30,
                MaxWeight = 105,
                Ratings = new List<Rating>(),

                Orders = new List<Order>(),

                CategoryId = null
            }
        );
    }

    public static void SeedShops(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>().HasData(
            new Shop()
            {
                Id = Guid.NewGuid(),
                Address = "Street Victory 5",

                Orders = new List<Order>(),
                WorkTimeStart = new TimeSpan(8, 0, 0),
                WorkTimeEnd = new TimeSpan(18, 0, 0)
            },
            new Shop()
            {
                Id = Guid.NewGuid(),
                Address = "Street Livikovicha 15",

                Orders = new List<Order>(),
                WorkTimeStart = new TimeSpan(8, 0, 0),
                WorkTimeEnd = new TimeSpan(18, 0, 0)
            },
            new Shop()
            {
                Id = Guid.NewGuid(),
                Address = "Street Chresatic 55",

                Orders = new List<Order>(),
                WorkTimeStart = new TimeSpan(8, 0, 0),
                WorkTimeEnd = new TimeSpan(18, 0, 0)
            }
        );
    }

    public static void SeedCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                Id = Guid.NewGuid(),
                Name_Category = "Scooter",
                Transports = new List<Transport>()
            },
            new Category()
            {
                Id = Guid.NewGuid(),
                Name_Category = "Bike",
                Transports = new List<Transport>()
            },
            new Category()
            {
                Id = Guid.NewGuid(),
                Name_Category = "Motorbike",
                Transports = new List<Transport>()
            }
        );
    }

    public static void SeedTransactions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 125,
                Date = DateTime.Now,
                OrderId = null,


            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 25,
                Date = DateTime.Now,
                OrderId = null,


            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 120,
                Date = DateTime.Now,
                OrderId = null,


            }
            );
    }

    public static void SeedLogTransactions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogTransaction>().HasData(
            new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = true,
                TransactionId = null,

            },
            new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = true,
                TransactionId = null,

            },
            new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = false,
                TransactionId = null,

            }
            );

    }

    public static void SeedTransportAvailable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransportAvailable>().HasData(
            new TransportAvailable
            {
                Id = Guid.NewGuid(),
                CountTransport = 15,
                TransportId = null,

                ShopId = null,

            },
            new TransportAvailable
            {
                Id = Guid.NewGuid(),
                CountTransport = 22,
                TransportId = null,

                ShopId = null,

            },
            new TransportAvailable
            {
                Id = Guid.NewGuid(),
                CountTransport = 14,
                TransportId = null,

                ShopId = null,

            }
            );
    }
    public static void SeedRating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rating>().HasData(
            new Rating
            {
                Id = Guid.NewGuid(),
                Grand = 2,
                Comment = "Bad",
                CreatedAt = DateTime.UtcNow,
                UserId = null,

                TransportId = null,


            },
            new Rating
            {
                Id = Guid.NewGuid(),
                Grand = 5,
                Comment = "Good",
                CreatedAt = DateTime.UtcNow,
                UserId = null,

                TransportId = null,


            },
            new Rating
            {
                Id = Guid.NewGuid(),
                Grand = 4,
                Comment = "Nice",
                CreatedAt = DateTime.UtcNow,
                UserId = null,

                TransportId = null,


            }
            );
    }
}
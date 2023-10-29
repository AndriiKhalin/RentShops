using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;


using System;
using RentShop_API.Models.Data;
using RentShop_API.Models.Entities;

namespace RentShop.Models;

public static class SeedData
{


    public static void SeedUsers(RentDbContext context)
    {

        if (!context.Users.Any())
        {

            var users = new List<User>
            {
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

            };
            context.AddRange(users);
            context.SaveChanges();

        }

        /*modelBuilder.Entity<User>().HasData(
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
        );*/
    }

    public static void SeedOrders(RentDbContext context)
    {
        if (!context.Orders.Any())
        {

            var orders = new List<Order>
            {
                new Order
                {
                    Id = Guid.NewGuid(),
                    DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 55, 03),
                    DateTo = DateTime.Now,
                    Price = 55f,
                    Shops = new List<Shop>(),
                    Transports = new List<Transport>(),
                    UserId = context.Users.FirstOrDefault(x=>x.Name=="Andrew")?.Id

                },
                new Order
                {
                    Id = Guid.NewGuid(),
                    DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 33, 13),
                    DateTo = DateTime.Now,
                    Price = 35f,
                    Shops = new List<Shop>(),
                    Transports = new List<Transport>(),
                    UserId = context.Users.FirstOrDefault(x=>x.Name=="Andrew")?.Id

                },
                new Order
                {
                    Id = Guid.NewGuid(),
                    DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 25, 53),
                    DateTo = DateTime.Now,
                    Price = 155f,
                    Shops = new List<Shop>(),
                    Transports = new List<Transport>(),
                    UserId = context.Users.FirstOrDefault(x => x.Name == "Vlad") ?.Id

                }

            };
            context.AddRange(orders);
            context.SaveChanges();

        }

        /*
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
        );*/
    }

    public static void SeedTransports(RentDbContext context)
    {

        if (!context.Transports.Any())
        {

            var transports = new List<Transport>
            {
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

            };
            context.AddRange(transports);
            context.SaveChanges();

        }

        /*
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
        );*/
    }

    public static void SeedShops(RentDbContext context)
    {

        if (!context.Shops.Any())
        {

            var shops = new List<Shop>
            {
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

            };
            context.AddRange(shops);
            context.SaveChanges();

        }

        /*
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
        );*/
    }

    public static void SeedCategories(RentDbContext context)
    {

        if (!context.Categories.Any())
        {

            var categories = new List<Category>
            {
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

            };
            context.AddRange(categories);
            context.SaveChanges();

        }
        /*
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
        );*/
    }

    public static void SeedTransactions(RentDbContext context)
    {
        if (!context.Transactions.Any())
        {

            var transactions = new List<Transaction>
            {
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

            };
            context.AddRange(transactions);
            context.SaveChanges();

        }


        /*
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
            );*/
    }

    public static void SeedLogTransactions(RentDbContext context)
    {


        if (!context.LogTransactions.Any())
        {

            var logTransactions = new List<LogTransaction>
            {
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

            };
            context.AddRange(logTransactions);
            context.SaveChanges();

        }

        /*
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
            );*/

    }

    public static void SeedTransportAvailable(RentDbContext context)
    {
        if (!context.TransportAvailables.Any())
        {

            var transportAvailables = new List<TransportAvailable>
            {
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

            };
            context.AddRange(transportAvailables);
            context.SaveChanges();

        }


        /*
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
            );*/
    }
    public static void SeedRating(RentDbContext context)
    {

        if (!context.Ratings.Any())
        {

            var ratings = new List<Rating>
            {
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

            };
            context.AddRange(ratings);
            context.SaveChanges();

        }


        /*
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
            );*/
    }
}
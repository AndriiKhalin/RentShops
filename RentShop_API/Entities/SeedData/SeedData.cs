using Entities.Models;

namespace Entities.SeedData;

public class SeedData
{
    public static void SeedDates(RentDbContext context)
    {

        //context.Database.EnsureDeleted();

        //context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            //User
            User andrew = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andrew",
                LastName = "Khalin",
                Email = "khalin2002@gmail.com",
                Password = "10122002",
                Phone = "+380737303288",
                BirthDate = new DateTime(2002, 12, 10),
                Role = Role.Client.ToString(),
                ImgUrl = "http://..."
            };
            User vanya = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Vanya",
                LastName = "Lebid",
                Email = "lebid@gmail.com",
                Password = "1920202",
                Phone = "+380737303277",
                BirthDate = new DateTime(2001, 10, 5),
                Role = Role.Client.ToString(),
                ImgUrl = "http://..."
            };
            User vlad = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Vlad",
                LastName = "Shabaltas",
                Email = "shabaltas@gmail.com",
                Password = "648577",
                Phone = "+380991833277",
                BirthDate = new DateTime(2004, 2, 18),
                Role = Role.Admin.ToString(),
                ImgUrl = "http://..."
            };
            context.Users.AddRange(andrew, vlad, vanya);


            //Category
            TransportCategory bike = new TransportCategory()
            {
                Id = Guid.NewGuid(),
                Name_Categories = "Bike",
            };
            TransportCategory scooter = new TransportCategory()
            {
                Id = Guid.NewGuid(),
                Name_Categories = "Scooter",
            };

            TransportCategory motorbike = new TransportCategory()
            {
                Id = Guid.NewGuid(),
                Name_Categories = "Motorbike",
            };

            context.TransportCategories.AddRange(bike, scooter, motorbike);


            //Transport
            Transport honda = new Transport()
            {
                Id = Guid.NewGuid(),
                Mark = "Honda",
                Model = "V3",
                PriceMinute = 3.5f,
                ImgUrl = "http://...",
                MaxSpeed = 35,
                MaxWeight = 125,
                TransportCategory = bike
            };

            Transport tesla = new Transport()
            {
                Id = Guid.NewGuid(),
                Mark = "Tesla",
                Model = "Skod",
                PriceMinute = 4.5f,
                ImgUrl = "http://...",
                MaxSpeed = 45,
                MaxWeight = 115,
                TransportCategory = scooter
            };
            Transport volva = new Transport()
            {
                Id = Guid.NewGuid(),
                Mark = "Volva",
                Model = "Speed",
                PriceMinute = 3f,
                ImgUrl = "http://...",
                MaxSpeed = 30,
                MaxWeight = 105,
                TransportCategory = motorbike
            };

            context.Transports.AddRange(honda, tesla, volva);


            //Shop
            Shop shop1 = new Shop()
            {
                Id = Guid.NewGuid(),
                Address = "Street Victory 5",
                WorkTimeStart = new TimeSpan(8, 0, 0),
                WorkTimeEnd = new TimeSpan(18, 0, 0),
                ImgUrl = "http://..."
            };

            Shop shop2 = new Shop()
            {
                Id = Guid.NewGuid(),
                Address = "Street Livikovicha 15",
                WorkTimeStart = new TimeSpan(8, 0, 0),
                WorkTimeEnd = new TimeSpan(18, 0, 0),
                ImgUrl = "http://..."
            };
            Shop shop3 = new Shop()
            {
                Id = Guid.NewGuid(),
                Address = "Street Chresatic 55",
                WorkTimeStart = new TimeSpan(8, 0, 0),
                WorkTimeEnd = new TimeSpan(18, 0, 0),
                ImgUrl = "http://..."
            };

            context.Shops.AddRange(shop1, shop2, shop3);


            //TransportAvailable
            TransportAvailable transportAvailable1 = new TransportAvailable
            {
                Id = Guid.NewGuid(),
                CountTransport = 15,
                Transport = honda,
                Shop = shop1
            };
            TransportAvailable transportAvailable2 = new TransportAvailable
            {
                Id = Guid.NewGuid(),
                CountTransport = 15,
                Transport = volva,
                Shop = shop2
            };
            TransportAvailable transportAvailable3 = new TransportAvailable
            {
                Id = Guid.NewGuid(),
                CountTransport = 15,
                Transport = tesla,
                Shop = shop3
            };

            context.TransportAvailables.AddRange(transportAvailable1, transportAvailable2, transportAvailable3);

            //Order
            Order order1 = new Order
            {
                Id = Guid.NewGuid(),
                CreatedUpdatedAt = DateTime.Now,
                OrderDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 55, 03),
                OrderDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 44, 03),
                Price = 55f,
                User = andrew,
                Transport = honda,
                Shop = shop1,
                TransportImgUrl = "http://..."
            };
            Order order2 = new Order
            {
                Id = Guid.NewGuid(),
                CreatedUpdatedAt = DateTime.Now,
                OrderDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 33, 13),
                OrderDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 55, 03),
                Price = 35f,
                User = vlad,
                Transport = tesla,
                Shop = shop2,
                TransportImgUrl = "http://..."
            };

            Order order3 = new Order
            {
                Id = Guid.NewGuid(),
                CreatedUpdatedAt = DateTime.Now,
                OrderDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 25, 53),
                OrderDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 05, 03),
                Price = 155f,
                User = vanya,
                Transport = volva,
                Shop = shop3,
                TransportImgUrl = "http://..."
            };
            Order order4 = new Order
            {
                Id = Guid.NewGuid(),
                CreatedUpdatedAt = DateTime.Now,
                OrderDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 25, 53),
                OrderDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 55, 03),
                Price = 55f,
                User = vanya,
                Transport = volva,
                Shop = shop1,
                TransportImgUrl = "http://..."

            };

            Order order5 = new Order
            {
                Id = Guid.NewGuid(),
                CreatedUpdatedAt = DateTime.Now,
                OrderDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 25, 53),
                OrderDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 55, 03),
                Price = 55f,
                User = vanya,
                Transport = volva,
                Shop = shop1,
                TransportImgUrl = "http://..."

            };

            context.Orders.AddRange(order1, order2, order3, order4, order5);


            //Transaction
            Transaction transaction1 = new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 125,
                Date = DateTime.Now,
                Order = order1

            };
            Transaction transaction2 = new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 25,
                Date = DateTime.Now,
                Order = order2

            };
            Transaction transaction3 = new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 120,
                Date = DateTime.Now,
                Order = order3

            };
            Transaction transaction4 = new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 150,
                Date = DateTime.Now,
                Order = order4

            };
            Transaction transaction5 = new Transaction
            {
                Id = Guid.NewGuid(),
                Sum = 250,
                Date = DateTime.Now,
                Order = order5

            };
            context.Transactions.AddRange(transaction1, transaction2, transaction3, transaction4, transaction5);


            //LogTransaction
            LogTransaction logTransaction1 = new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = true,
                Transaction = transaction1
            };
            LogTransaction logTransaction2 = new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = true,
                Transaction = transaction2
            };
            LogTransaction logTransaction3 = new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = true,
                Transaction = transaction3
            };
            LogTransaction logTransaction4 = new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = false,
                Transaction = transaction4
            };
            LogTransaction logTransaction5 = new LogTransaction
            {
                Id = Guid.NewGuid(),
                Results = false,
                Transaction = transaction5
            };
            context.LogTransactions.AddRange(logTransaction1, logTransaction2, logTransaction3, logTransaction4,
                logTransaction5);


            //Rating
            Rating rating1 = new Rating
            {
                Id = Guid.NewGuid(),
                Grand = 2,
                Comment = "Bad",
                CreatedUpdatedAt = DateTime.UtcNow,
                User = andrew,
                Transport = volva
            };
            Rating rating2 = new Rating
            {
                Id = Guid.NewGuid(),
                Grand = 5,
                Comment = "Good",
                CreatedUpdatedAt = DateTime.UtcNow,
                User = vlad,
                Transport = tesla
            };
            Rating rating3 = new Rating
            {
                Id = Guid.NewGuid(),
                Grand = 4,
                Comment = "Nice",
                CreatedUpdatedAt = DateTime.UtcNow,
                User = vlad,
                Transport = volva
            };

            context.Ratings.AddRange(rating1, rating2, rating3);

            context.SaveChanges();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

using RentShop_API.Models.Domain.Clients;
using RentShop_API.Models.Domain.Products;
using RentShop_API.Models.Domain;
using System;
using RentShop_API.Models.Data;

namespace RentShop.Models;

public static class SeedData
{
    public static void SeedClients(RentDbContext context)
    {
        if (!context.Clients.Any())
        {
            var clients = new List<Client>
            {
                new Client(){Name="Andrew",Status = context.Status.FirstOrDefault(x => x.Name == "Client"),SurName = "Khalin"},
                new Client(){Name="Ihor",Status = context.Status.FirstOrDefault(x => x.Name == "Admin"),SurName = "Khalin"},
                new Client(){Name="Nadiia",Status = context.Status.FirstOrDefault(x => x.Name == "Admin"),SurName = "Khalina"},
                new Client(){Name="Pavel",Status =  context.Status.FirstOrDefault(x => x.Name == "Client"),SurName = "Krab"},
                new Client(){Name="Dima",Status =  context.Status.FirstOrDefault(x => x.Name == "Client"),SurName = "Krut"}

            };
            context.AddRange(clients);
            context.SaveChanges();
        }
    }

    public static void SeedBikes(RentDbContext context)
    {
        if (!context.Bikes.Any())
        {
            var bikes = new List<Bikes>
            {
                new Bikes()
                {
                    Name="Road",
                    Price=54.668667M,
                    Description = "Road bikes are best identified by their drop or turned-down handlebars and skinny tires.",
                    MaxWeight = 110,
                    Speed = 35,
                    ImageUrl = "https"
                },
                new Bikes()
                {
                    Name="Mountain",
                    Price=100.2M,
                    Description = @"This bike is designed with excellent braking systems and shock-absorbing features that can easily handle serious bumps,
                       rocks, dirt trails, roots and ruts.",
                    MaxWeight = 120,
                    Speed = 40,
                    ImageUrl = "http"
                },
                new Bikes()
                {
                Name="Touring",
                Price=44M,
                Description = @"These are almost like the traditional road bikes,
                except with a few tweaks and changes that make them perfect for long-distance bike tours.",
                MaxWeight = 90,
                Speed = 50,
                ImageUrl = "https"
                }

            };
            context.AddRange(bikes);
            context.SaveChanges();
        }
    }

    public static void SeedScooters(RentDbContext context)
    {
        if (!context.Scooters.Any())
        {
            var scooters = new List<Scooters>
            {
                new Scooters()
                {
                    Name="Honda Activa 6G",
                    Price=76.234M,
                    Description = @"Activa 6G is powered by a 109cc BS6 engine. This engine of the Activa 6G generates power of 7.79 ps and torque of 8.84 nm . 
                     The Honda Activa 6G gets drum brakes in the front and rear.
                     The Honda Activa 6G weighs 105 kg and has a fuel tank capacity of 5 liters.",
                    MaxWeight = 120,
                    Speed = 66,
                    ImageUrl = "http"
                },
                new Scooters()
                {
                    Name="TVS NTORQ 125",
                    Price=100.2M,
                    Description = @"The TVS NTORQ 125 is a scooter in a price range of Rs. 84,536 to 1.04 Lakh in Indian Market. It is offered in six variants and twelve colours.",
                    MaxWeight = 100,
                    Speed = 75,
                    ImageUrl = "https"
                },
                new Scooters()
                {
                    Name="Suzuki Access 125",
                    Price=44M,
                    Description = @"EMI starts from Rs. 2,287",
                    MaxWeight = 90,
                    Speed = 70,
                    ImageUrl = "https"
                }

            };
            context.AddRange(scooters);
            context.SaveChanges();
        }
    }

    public static void SeedStatus(RentDbContext context)
    {
        if (!context.Status.Any())
        {
            var status = new List<Status>
            {
                new Status(){Name="Client"},
                new Status(){Name="Admin"}

            };
            context.AddRange(status);
            context.SaveChanges();
        }
    }
}
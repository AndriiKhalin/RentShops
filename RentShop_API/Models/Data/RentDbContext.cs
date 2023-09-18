using Microsoft.EntityFrameworkCore;
using RentShop_API.Models.Domain.Clients;
using RentShop_API.Models.Domain.Products;
using RentShop_API.Models.Domain;

namespace RentShop_API.Models.Data;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions opt) : base(opt)
    {

    }
    public DbSet<Bikes> Bikes { get; set; }

    public DbSet<Scooters> Scooters { get; set; }
    public DbSet<Client> Clients { get; set; }

    public DbSet<Status> Status { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    //var configuration = new ConfigurationBuilder()
    //    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    //    .AddJsonFile("appsettings.json")
    //    //    .Build();

    //    //var connectionString = configuration.GetConnectionString("AppDb");
    //    //optionsBuilder.UseSqlServer(connectionString);


    //}

}
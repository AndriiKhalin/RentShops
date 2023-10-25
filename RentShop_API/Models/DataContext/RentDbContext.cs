using Microsoft.EntityFrameworkCore;
using RentShop_API.Models.Entities;


namespace RentShop_API.Models.Data;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions opt) : base(opt)
    {

    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    //var configuration = new ConfigurationBuilder()
    //    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    //    .AddJsonFile("appsettings.json")
    //    //    .Build();

    //    //var connectionString = configuration.GetConnectionString("AppDb");
    //    //optionsBuilder.UseSqlServer(connectionString);


    //}

    public DbSet<Transaction> Transactions { get; set; } = null!;

    public DbSet<Rating> Ratings { get; set; } = null!;

    public DbSet<LogTransaction> LogTransactions { get; set; } = null!;

    public DbSet<Shop> Shops { get; set; } = null!;

    public DbSet<TransportAvailable> TransportAvailables { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Transport> Transports { get; set; } = null!;
}
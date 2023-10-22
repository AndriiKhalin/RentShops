using Microsoft.EntityFrameworkCore;


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

}
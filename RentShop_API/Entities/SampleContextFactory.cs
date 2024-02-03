using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities;

public class SampleContextFactory : IDesignTimeDbContextFactory<RentDbContext>
{
    public RentDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RentDbContext>();



        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();

        // получаем строку подключения из файла appsettings.json
        var connectionString = config.GetConnectionString("AppDb");
        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("RentShop_API"));
        return new RentDbContext(optionsBuilder.Options);
    }
}
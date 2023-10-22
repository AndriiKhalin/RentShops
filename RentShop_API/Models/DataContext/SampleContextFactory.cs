using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace RentShop_API.Models.Data;

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
        optionsBuilder.UseSqlServer(connectionString);
        return new RentDbContext(optionsBuilder.Options);
    }
}
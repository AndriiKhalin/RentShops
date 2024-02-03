using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RentDbContext : DbContext
{
    public RentDbContext(DbContextOptions opt) : base(opt)
    {

    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(b => b.MigrationsAssembly("RentShop_API"));
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region SetNullDeleteBehavior

        modelBuilder.Entity<Order>()
            .HasOne(x => x.User)
            .WithMany(y => y.Orders)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(x => x.Transport)
            .WithMany(y => y.Orders)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(x => x.Shop)
            .WithMany(y => y.Orders)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Order>()
            .HasOne(x => x.Transaction)
            .WithOne(y => y.Order)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Rating>()
            .HasOne(x => x.User)
            .WithMany(y => y.Ratings)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Rating>()
            .HasOne(x => x.Transport)
            .WithMany(y => y.Ratings)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.Order)
            .WithOne(y => y.Transaction)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<LogTransaction>()
            .HasOne(x => x.Transaction)
            .WithOne(y => y.LogTransaction)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Transport>()
            .HasOne(x => x.Category)
            .WithMany(y => y.Transports)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TransportAvailable>()
            .HasOne(x => x.Transport)
            .WithMany(y => y.TransportAvailables)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<TransportAvailable>()
            .HasOne(x => x.Shop)
            .WithMany(y => y.TransportAvailables)
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

    }

    public DbSet<Transaction>? Transactions { get; set; } = null!;

    public DbSet<Rating>? Ratings { get; set; } = null!;

    public DbSet<LogTransaction>? LogTransactions { get; set; } = null!;

    public DbSet<Shop>? Shops { get; set; } = null!;

    public DbSet<TransportAvailable>? TransportAvailables { get; set; } = null!;
    public DbSet<User>? Users { get; set; } = null!;
    public DbSet<Category>? Categories { get; set; } = null!;
    public DbSet<Order>? Orders { get; set; } = null!;
    public DbSet<Transport>? Transports { get; set; } = null!;


}
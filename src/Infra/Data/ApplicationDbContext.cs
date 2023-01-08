using System;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnsualLotery.Domain.Lotery;

namespace UnsualLotery.Infra.Data; 

public class ApplicationDbContext : IdentityDbContext<IdentityUser> {

    public DbSet<Quota> Quotas { get; set; }
    public DbSet<Raffle> Raffles { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {

        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        // Quotas
        builder.Entity<Quota>()
            .Property(q => q.QuotasAmount).IsRequired();

        builder.Entity<Quota>()
            .Property(q => q.QuotasNumbers).IsRequired();

        builder.Entity<Quota>()
            .Property(q => q.Cost).HasColumnType("decimal(10,2)").IsRequired();

        // Raffles
        builder.Entity<Raffle>()
            .Property(r => r.Value).HasColumnType("decimal(10,2)").IsRequired();

        builder.Entity<Raffle>()
            .Property(r => r.AvailableQuantity).HasColumnType("int").IsRequired();

        builder.Entity<Raffle>()
            .Property(r => r.TotalQuantity).HasColumnType("int").IsRequired();

        builder.Entity<Raffle>()
            .Property(r => r.Active).IsRequired();

        builder.Entity<Raffle>()
            .HasMany<Quota>(r => r.Quota)
            .WithOne(q => q.Raffle)
            .HasForeignKey(q => q.RaffleId);

        // Quotas Purcache
        builder.Entity<Purchase>()
            .Property(p => p.Value).HasColumnType("decimal(10,2)").IsRequired();

        builder.Entity<Purchase>()
            .Property(p => p.QuotasAmount).HasColumnType("int").IsRequired();
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration) {

        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}
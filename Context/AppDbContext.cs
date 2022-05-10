﻿using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.ViewModels;
using SocialRentAccunting.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialRentAccunting.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<House> Houses { get; set; }
        public DbSet<Kinship> Kinships { get; set; }
        public DbSet<Kinsman> Kinsmen { get; set; }
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(ContractConfigure);
            modelBuilder.Entity<Kinsman>().HasOne(k => k.Tenant).WithMany(t => t.Kinsmen).HasForeignKey(k => k.TenantId);

            modelBuilder.Entity<Tenant>().OwnsOne(t => t.Passport);

            modelBuilder.Entity<Contract>().HasOne(c => c.Tenant).WithMany(t => t.Contracts).HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Contract>().HasOne(c => c.Landlord).WithMany(l => l.Contracts).HasForeignKey(c => c.LandlordId);
            modelBuilder.Entity<Contract>().HasOne(c => c.House).WithMany(h => h.Contracts).HasForeignKey(c => c.HouseId);

            modelBuilder.Entity<Order>().HasOne(o => o.Contract).WithOne(c => c.Order).HasForeignKey<Contract>(c => c.OrderId);
        }

        private void ContractConfigure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasIndex(c => c.Number).IsUnique();
            builder.Property(c => c.DateStart).HasColumnType("date");
            builder.Property(c => c.DateEnd).HasColumnType("date"); 
        }
    }
}

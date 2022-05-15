using Microsoft.EntityFrameworkCore;
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
        public DbSet<Role> Roles { get; set; }

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

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role {Id = 1, Name = "Admin"},
                new Role {Id = 2, Name = "Operator"}
            });

            modelBuilder.Entity<User>().HasData(new User { Id=1, Name="Admin", Password="Admin", RoleId=1});

            modelBuilder.Entity<Kinship>().HasData(new Kinship[]
            {
                new Kinship {Id = 1, Name = "муж"},
                new Kinship {Id = 2, Name = "жена"},
                new Kinship {Id = 3, Name = "сын"},
                new Kinship {Id = 4, Name = "дочь"},
                new Kinship {Id = 5, Name = "отец"},
                new Kinship {Id = 6, Name = "мать"},
                new Kinship {Id = 7, Name = "брат"},
                new Kinship {Id = 8, Name = "сестра"}
            });
        }

        private void ContractConfigure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasIndex(c => c.Number).IsUnique();
        }
    }
}

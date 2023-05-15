using LaptopStore.Data.Configurations;
using LaptopStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Context
{
    public class LaptopStoreDbContext : DbContext
    {
        public LaptopStoreDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected LaptopStoreDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new NotifyConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SeriesConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserBehaviorTrackerConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new JWTTokenConfiguration());
            modelBuilder.ApplyConfiguration(new OTPConfiguration());

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = new Guid("6FD0F97A-1522-475C-ABA1-92F3CE5AEB04"), Name = "Admin" },
                new Role { Id = new Guid("A1D06430-35AF-433A-AEFB-283F559059FB"), Name = "Employee"},
                new Role { Id = new Guid("116E0DEB-F72F-45CF-8EF8-423748B8E9B1"), Name = "Customer" }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = new Guid("597C8190-753D-4BB4-9253-C23BFE7D192C"), Name = "Ly", Email = "ly@gmail.com", Phone = "0775678910", Password = BCrypt.Net.BCrypt.HashPassword("123456789"), Img = "", RoleId = new Guid("6FD0F97A-1522-475C-ABA1-92F3CE5AEB04") }
                );
        }

        public DbSet<Advertisement> Advertisements => Set<Advertisement>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Notify> Notices => Set<Notify>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Series> Series => Set<Series>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<UserBehaviorTracker> UserBehaviorTrackers => Set<UserBehaviorTracker>();
        public DbSet<User> Users => Set<User>();
        public DbSet<JwTToken> jwTTokens => Set<JwTToken>();
        public DbSet<OTP> OTPs => Set<OTP>();
    }
}

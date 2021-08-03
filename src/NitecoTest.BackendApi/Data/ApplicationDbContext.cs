using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NitecoTest.BackendApi.Data.Entities;

namespace NitecoTest.BackendApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Category> Categorys { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>().HasOne(o => o.Product)
                .WithMany(x => x.Orders).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<Order>().HasOne(o => o.Customer)
                .WithMany(x => x.Orders).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            builder.Entity<Product>().HasOne(p => p.Category)
                .WithMany(x => x.Products).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}

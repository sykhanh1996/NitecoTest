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

            #region Configuration

            builder.Entity<Order>()
                .HasKey(c => new { c.CustomerId, c.ProductId });

            #endregion Identity Configuration

     
        }
    }
}

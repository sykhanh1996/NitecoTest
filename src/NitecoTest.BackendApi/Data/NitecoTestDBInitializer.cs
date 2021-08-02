using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NitecoTest.BackendApi.Data.Entities;

namespace NitecoTest.BackendApi.Data
{
    public class NitecoTestDBInitializer
    {
        private readonly ApplicationDbContext _context;
        public NitecoTestDBInitializer(ApplicationDbContext context
           )
        {
            _context = context;
        }
        public async Task Seed()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(new List<Customer>
                {
                    new Customer
                    {
                        Name = "Sy Khanh",
                        Address="Ha Noi"
                    },
                    new Customer
                    {
                        Name = "Niteco",
                        Address="Ha Noi"
                    },
                    new Customer
                    {
                        Name = "Niteco Test",
                        Address="Ha Noi"
                    }
                });
                await _context.SaveChangesAsync();
            }
            if (!_context.Categorys.Any())
            {
                _context.Categorys.AddRange(new List<Category>
                {
                    new Category
                    {
                       Name = "Shoes",
                       Description = "This is Shoes"
                    },
                    new Category
                    {
                        Name = "Computer",
                        Description = "This is Computer"
                    },
                    new Category
                    {
                        Name = "Water",
                        Description = "This is Water"
                    }
                });
                await _context.SaveChangesAsync();
            }
            if (!_context.Products.Any())
            {
                if (_context.Categorys.Any())
                {
                    var category1 = await _context.Categorys.FirstOrDefaultAsync(x => x.Name.Equals("Shoes"));
                    var category2 = await _context.Categorys.FirstOrDefaultAsync(x => x.Name.Equals("Computer"));
                    var category3 = await _context.Categorys.FirstOrDefaultAsync(x => x.Name.Equals("Water"));
                    _context.Products.AddRange(new List<Product>
                    {
                        new Product
                        {
                            Name = "Nike's Shoes",
                            Description = "Nike's Shoes",
                            Price = 20000,
                            CategoryId = category1.Id,
                            Quantity = 5
                        },
                        new Product
                        {
                            Name = "Addidas's Shoes",
                            Description = "Addidas's Shoes",
                            Price = 90000,
                            CategoryId = category1.Id,
                            Quantity = 100
                        },
                        new Product
                        {
                            Name = "Laptop",
                            Description = "Laptop",
                            Price = 10000000,
                            CategoryId = category2.Id,
                            Quantity = 50
                        },
                        new Product
                        {
                            Name = "Fresh",
                            Description = "Fresh",
                            Price = 10000,
                            CategoryId = category3.Id,
                            Quantity = 1000
                        }
                    });
                    await _context.SaveChangesAsync();
                }
            }

            if (!_context.Orders.Any())
            {
                if (_context.Customers.Any() && _context.Products.Any())
                {
                    var customer = await _context.Customers.ToListAsync();
                    var product = await _context.Products.ToListAsync();
                    _context.Orders.AddRange(new List<Order>
                    {
                        new Order
                        {
                            CustomerId = customer[0].Id,
                            ProductId = product[0].Id,
                            Amount = 1,
                            OrderDate = DateTime.Now
                        },
                        new Order
                        {
                            CustomerId = customer[1].Id,
                            ProductId = product[1].Id,
                            Amount = 1,
                            OrderDate = DateTime.Now
                        },
                        new Order
                        {
                            CustomerId = customer[2].Id,
                            ProductId = product[2].Id,
                            Amount = 1,
                            OrderDate = DateTime.Now
                        },
                    });
                }
            }
        }
    }
}

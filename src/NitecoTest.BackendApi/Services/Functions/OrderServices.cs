using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Helpers;
using NitecoTest.BackendApi.Services.Interfaces;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Services.Functions
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderServices> _logger;

        public OrderServices(ILogger<OrderServices> logger,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<OrdersVm>> GetOrdersPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Product.Name.Contains(filter) || x.Product.Category.Name.Contains(filter) || x.Customer.Name.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.OrderByDescending(x => x.CustomerId).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(o => new OrdersVm
                {
                    ProductName = o.Product.Name,
                    CategoryName = o.Product.Category.Name,
                    CustomerName = o.Customer.Name,
                    OrderDate = o.OrderDate.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture),
                    Amount = o.Amount
                }).ToListAsync();
            return items;
        }
    }
}

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
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.BackendApi.Helpers;
using NitecoTest.BackendApi.Services.Interfaces;
using NitecoTest.ViewModels;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Services.Functions
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderServices> _logger;
        private readonly IMapper _mapper;

        public OrderServices(ILogger<OrderServices> logger,
            ApplicationDbContext context,
            IConfiguration configuration,
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<bool> CheckOrderQuantity(OrderCreateRequest orderCreateRequest)
        {
            var quantity = await (from o in _context.Orders
                join p in _context.Products on o.ProductId equals p.Id
                select new {p.Quantity}).FirstOrDefaultAsync();

            if (quantity == null)
                return true;
            if (quantity.Quantity > orderCreateRequest.Amount)
                return false;
         
            return true;
        }

        public async Task<bool> CreateOrder(OrderCreateRequest orderCreateRequest)
        {
            var order = new Order
            {
                CustomerId = orderCreateRequest.CustomerId,
                ProductId = orderCreateRequest.ProductId,
                Amount = orderCreateRequest.Amount,
                OrderDate = Convert.ToDateTime(orderCreateRequest.OrderDate)
            };
            await _context.Orders.AddAsync(order);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }

        public async Task<List<CustomerVm>> GetAllCustomerVm()
        {
            var customer = _context.Customers.AsQueryable();
            var customerVm = await _mapper.ProjectTo<CustomerVm>(customer).ToListAsync();
            return customerVm;
        }

        public async Task<List<ProductVm>> GetAllProductVm()
        {
            var product = _context.Products.AsQueryable();
            var productVm = await _mapper.ProjectTo<ProductVm>(product).ToListAsync();
            return productVm;
        }

        public async Task<Pagination<OrdersVm>> GetOrdersPaging(string filter, int pageIndex, int pageSize)
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
            var pagination = new Pagination<OrdersVm>
            {
                Items = items,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalRecords = totalRecords,
            };
            return pagination;
        }
    }
}

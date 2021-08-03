using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NitecoTest.BackendApi.Automapper;
using NitecoTest.BackendApi.Controllers;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.BackendApi.Services.Functions;
using NitecoTest.ViewModels.Contents;
using Xunit;

namespace NitecoTest.BackendApi.UnitTest.Services.Functions
{
    public class OrderServicesTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderServicesTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ViewModelToDomainMappingProfile());
                    mc.AddProfile(new DomainToViewModelMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [Fact]
        public void OrdersController_ShouldCreateInstance_NotNull_Success()
        {
            var controller = new OrderServices(_context, _mapper);
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task GetAllCustomerVm_GetInstance_Success()
        {
            _context.Customers.AddRange(new List<Customer>
            {
                new Customer
                {
                    Name="Sy Khanh",
                    Address = "Ha Noi"
                },
                new Customer
                {
                    Name = "Niteco",
                    Address = "Ha Noi"
                },
                new Customer
                {
                    Name = "Niteco",
                    Address = "Ha Noi"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new OrderServices(_context, _mapper);
            var result = await controller.GetAllCustomerVm();
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetAllCustomerVm_GetInstance_Fail()
        {
            var controller = new OrderServices(_context, _mapper);
            var result = await controller.GetAllCustomerVm();
            Assert.True(!result.Any());
        }

        [Fact]
        public async Task GetAllProductVm_GetInstance_Success()
        {
            _context.Products.AddRange(new List<Product>
            {
                new Product
                {
                    Name="Laptop",
                    CategoryId = 2,
                    Price = 10000,
                    Description = "Laptop",
                    Quantity = 50
                }
              
            });
            await _context.SaveChangesAsync();
            var controller = new OrderServices(_context, _mapper);
            var result = await controller.GetAllProductVm();
            Assert.True(result.Any());
        }
    }
}

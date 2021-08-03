using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NitecoTest.BackendApi.Controllers;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.BackendApi.Services.Interfaces;
using Xunit;

namespace NitecoTest.BackendApi.UnitTest.Controllers
{
    public class OrdersControllerTest
    {
        private ApplicationDbContext _context;
        private readonly IOrderServices _orderServices;

        public OrdersControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();
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
            _context.SaveChangesAsync().ConfigureAwait(true);
        }

        [Fact]
        public void OrdersController_ShouldCreateInstance_NotNull_Success()
        {
            var controller = new OrdersController(_orderServices);
            Assert.NotNull(controller);
        }
      
    }
}

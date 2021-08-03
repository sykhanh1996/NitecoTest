using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NitecoTest.WebPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using NitecoTest.ViewModels.Contents;
using NitecoTest.WebPortal.Services.Interfaces;

namespace NitecoTest.WebPortal.Controllers
{
   
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOrderApiClient _orderApiClient;

        public HomeController(ILogger<HomeController> logger,
            IConfiguration configuration,
            IOrderApiClient orderApiClient)
        {
            _logger = logger;
            _configuration = configuration;
            _orderApiClient = orderApiClient;
        }

        public async Task<IActionResult> Index(string filter, int page = 1)
        {
            var pageSize = int.Parse(_configuration["PageSize"]);
            var orders = await _orderApiClient.GetAllOrderPaging(filter, page, pageSize);
            var customers = await _orderApiClient.GetAllCustomer();
            var products = await _orderApiClient.GetAllProduct();
            var viewModel = new OrderViewModel()
            {
                Data = orders,
                lstCustomer = customers,
                lstProduct = products
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(OrderCreateRequest orderVm)
        {
            var checkQuantity = await _orderApiClient.CheckProductQuantity(orderVm);
            if (checkQuantity.Equals(false))
                return NotFound();
            
            var orders = await _orderApiClient.AddOrderRequest(orderVm);
            if (orders)
                return Ok();
            return BadRequest();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

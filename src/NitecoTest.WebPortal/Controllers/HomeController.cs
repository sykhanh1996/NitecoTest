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
using NitecoTest.WebPortal.Services.Interfaces;

namespace NitecoTest.WebPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
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

        public async Task<IActionResult> Index(string filter,int page = 1)
        {
            var pageSize = int.Parse(_configuration["PageSize"]);
            pageSize = 1;
            var orders = await _orderApiClient.GetAllOrderPaging(filter, page, pageSize);
            var viewModel = new OrderViewModel()
            {
                Data = orders
            };
            return View(viewModel);
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

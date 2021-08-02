using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Services.Interfaces;

namespace NitecoTest.BackendApi.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;
        public OrdersController(
            IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetOrder(string filter, int pageIndex = 1, int pageSize = 1)
        {
            var order = await _orderServices.GetOrdersPaging(filter, pageIndex, pageSize);
            return Ok(order);
        }
    }
}

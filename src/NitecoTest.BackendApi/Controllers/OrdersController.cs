using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Services.Interfaces;
using NitecoTest.ViewModels.Contents;

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
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var order = await _orderServices.GetAllCustomerVm();
            return Ok(order);
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProduct()
        {
            var order = await _orderServices.GetAllProductVm();
            return Ok(order);
        }
        [HttpPost("createOrder")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var order = await _orderServices.CreateOrder(orderCreateRequest);
            if (order)
                return Ok(true);
            return BadRequest(false);
        }

        [HttpPost("checkQuantity")]
        [Authorize]
        public async Task<IActionResult> CheckQuantity([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var order = await _orderServices.CheckOrderQuantity(orderCreateRequest);
            if (order)
                return Ok();
            return BadRequest();
        }
    }
}

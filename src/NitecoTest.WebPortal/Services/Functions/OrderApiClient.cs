using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NitecoTest.ViewModels;
using NitecoTest.ViewModels.Contents;
using NitecoTest.WebPortal.Services.Interfaces;

namespace NitecoTest.WebPortal.Services.Functions
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddOrderRequest(OrderCreateRequest orderCreateRequest)
        {
            var json = JsonConvert.SerializeObject(orderCreateRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.PostAsync($"/api/Orders/createOrder", httpContent);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> CheckProductQuantity(OrderCreateRequest orderCreateRequest)
        {
            var json = JsonConvert.SerializeObject(orderCreateRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.PostAsync($"/api/Orders/checkQuantity", httpContent);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<CustomerVm>> GetAllCustomer()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            var res = await client.GetAsync($"/api/Orders/customers");
            var customer = JsonConvert.DeserializeObject<List<CustomerVm>>(await res.Content.ReadAsStringAsync());
            return customer;
        }

        public async Task<Pagination<OrdersVm>> GetAllOrderPaging(string filter,int pageIndex, int pageSize)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            var res = await client.GetAsync($"/api/Orders/filter?filter={filter ?? string.Empty}&pageIndex={pageIndex}&pageSize={pageSize}");
            var posts = JsonConvert.DeserializeObject<Pagination<OrdersVm>>(await res.Content.ReadAsStringAsync());
            return posts;
        }

        public async Task<List<ProductVm>> GetAllProduct()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            var res = await client.GetAsync($"/api/Orders/products");
            var products = JsonConvert.DeserializeObject<List<ProductVm>>(await res.Content.ReadAsStringAsync());
            return products;
        }
    }
}

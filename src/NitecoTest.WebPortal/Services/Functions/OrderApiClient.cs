using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<Pagination<OrdersVm>> GetAllOrderPaging(string filter,int pageIndex, int pageSize)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            var res = await client.GetAsync($"/api/Orders/filter?filter={filter ?? string.Empty}&pageIndex={pageIndex}&pageSize={pageSize}");
            var posts = JsonConvert.DeserializeObject<Pagination<OrdersVm>>(await res.Content.ReadAsStringAsync());
            return posts;
        }
    }
}

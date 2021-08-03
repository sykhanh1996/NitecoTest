using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NitecoTest.ViewModels.Contents;
using NitecoTest.WebPortal.Helper;
using NitecoTest.WebPortal.Services.Interfaces;

namespace NitecoTest.WebPortal.Services.Functions
{
    public class LoginApiClient : ILoginApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResponse> Authenticate(CustomerCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BackendApiUrl"]);
            var response = await client.GetAsync($"/api/Customers/{request.CustomerName}/authenticate");
            if (response.IsSuccessStatusCode)
            {
                var apiSuccess = JsonConvert.DeserializeObject<ApiSuccessResponse<CustomerVm>>(await response.Content.ReadAsStringAsync());
                apiSuccess._objectResponse = JsonConvert.DeserializeObject<CustomerVm>(await response.Content.ReadAsStringAsync());
                return apiSuccess;
            }

            return JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}

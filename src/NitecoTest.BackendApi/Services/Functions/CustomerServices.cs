using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.BackendApi.Helpers;
using NitecoTest.BackendApi.Services.Interfaces;

namespace NitecoTest.BackendApi.Services.Functions
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerServices> _logger;

        public CustomerServices(ILogger<CustomerServices> logger,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ApiResponse> Authorization(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return new ApiBadRequestResponse("Customer Name cannot be empty");
            var user = await _context.Customers.FirstOrDefaultAsync(x => x.Name.Equals(userName.Trim()));
            if (user == null)
                return new ApiNotFoundResponse($"Cannot Find User: {userName}");
            return new ApiSuccessResponse<Customer>(user);
        }
    }
}

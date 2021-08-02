using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.BackendApi.Helpers;
using NitecoTest.BackendApi.Services.Interfaces;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Services.Functions
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerServices> _logger;
        private readonly IMapper _mapper;

        public CustomerServices(ILogger<CustomerServices> logger,
            ApplicationDbContext context,
            IConfiguration configuration,
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }
        public async Task<ApiResponse> Authenticate(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return new ApiBadRequestResponse("Customer Name cannot be empty");
            var user = await _context.Customers.FirstOrDefaultAsync(x => x.Name.Equals(userName.Trim()));
            var userVm = _mapper.Map<CustomerVm>(user);
            if (user == null)
                return new ApiNotFoundResponse($"Cannot Find User: {userName}");
            return new ApiSuccessResponse<CustomerVm>(userVm);
        }
    }
}

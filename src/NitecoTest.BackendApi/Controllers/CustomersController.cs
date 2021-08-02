using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NitecoTest.BackendApi.Data;
using NitecoTest.BackendApi.Data.Entities;
using NitecoTest.BackendApi.Helpers;
using NitecoTest.BackendApi.Services.Interfaces;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.BackendApi.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomerServices _customerServices;
        private readonly IConfiguration _config;
        public CustomersController(ICustomerServices customerServices,
            IConfiguration config)
        {
            _customerServices = customerServices;
            _config = config;
        }

        [HttpGet("{loginName}/authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authencate(string loginName)
        {
            var authentication = await _customerServices.Authenticate(loginName);
            if (!(authentication is ApiSuccessResponse<CustomerVm>))
                return BadRequest(new ApiBadRequestResponse("Error!"));
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var obj = new JwtSecurityTokenHandler().WriteToken(token);
            if (string.IsNullOrEmpty(obj))
            {
                return BadRequest(new ApiBadRequestResponse("Something bad happened!Please contact the administrator"));
            }

            var customerVm = authentication as ApiSuccessResponse<CustomerVm>;
            customerVm._objectResponse.Access_token = obj;
            return Ok(customerVm._objectResponse);
        }
    }
}

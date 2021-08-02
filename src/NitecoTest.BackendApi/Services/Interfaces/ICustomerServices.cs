using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.BackendApi.Helpers;

namespace NitecoTest.BackendApi.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<ApiResponse> Authorization(string userName);
    }
}

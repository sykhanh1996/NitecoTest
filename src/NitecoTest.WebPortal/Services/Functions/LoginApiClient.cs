using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.ViewModels.Contents;
using NitecoTest.WebPortal.Helper;
using NitecoTest.WebPortal.Services.Interfaces;

namespace NitecoTest.WebPortal.Services.Functions
{
    public class LoginApiClient : ILoginApiClient
    {
        public Task<ApiResponse> Authenticate(CustomerCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

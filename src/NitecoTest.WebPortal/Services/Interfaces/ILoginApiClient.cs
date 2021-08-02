using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.ViewModels.Contents;
using NitecoTest.WebPortal.Helper;

namespace NitecoTest.WebPortal.Services.Interfaces
{
    public interface ILoginApiClient
    {
        Task<ApiResponse> Authenticate(CustomerCreateRequest request);

    }
}

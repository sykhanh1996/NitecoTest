using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.ViewModels;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.WebPortal.Services.Interfaces
{
    public interface IOrderApiClient
    {
        Task<Pagination<OrdersVm>> GetAllOrderPaging(string filter,int pageIndex, int pageSize);
    }
}

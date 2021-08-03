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
        Task<List<CustomerVm>> GetAllCustomer();
        Task<List<ProductVm>> GetAllProduct();
        Task<bool> AddOrderRequest(OrderCreateRequest orderCreateRequest);
        Task<bool> CheckProductQuantity(OrderCreateRequest orderCreateRequest);
    }
}

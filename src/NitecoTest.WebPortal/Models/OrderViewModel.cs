using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.ViewModels;
using NitecoTest.ViewModels.Contents;

namespace NitecoTest.WebPortal.Models
{
    public class OrderViewModel
    {
        public Pagination<OrdersVm> Data { set; get; }
        public List<CustomerVm> lstCustomer { set; get; }
        public List<ProductVm> lstProduct { set; get; }
    }
}

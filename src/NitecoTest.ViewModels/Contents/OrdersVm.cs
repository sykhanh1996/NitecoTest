using System;
using System.Collections.Generic;
using System.Text;

namespace NitecoTest.ViewModels.Contents
{
    public class OrdersVm
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CustomerName { get; set; }
        public string OrderDate { get; set; }
        public int Amount { get; set; }
    }
}

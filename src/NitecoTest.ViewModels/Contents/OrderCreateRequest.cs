using System;
using System.Collections.Generic;
using System.Text;

namespace NitecoTest.ViewModels.Contents
{
    public class OrderCreateRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string OrderDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.BackendApi.Data.Interfaces;

namespace NitecoTest.BackendApi.Data.Entities
{
    [Table("Order")]
    public class Order 
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Product Product { set; get; }
        public virtual Customer Customer { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using NitecoTest.BackendApi.Data.Interfaces;

namespace NitecoTest.BackendApi.Data.Entities
{
    [Table("Product")]
    public class Product : IKeyTable<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; }

        public virtual ICollection<Order> Orders { set; get; }
    }
}

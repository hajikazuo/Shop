using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }
    }
}

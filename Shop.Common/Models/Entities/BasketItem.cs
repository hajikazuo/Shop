using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.Entities
{
    public class BasketItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}

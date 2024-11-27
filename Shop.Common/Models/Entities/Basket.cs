using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.Entities
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public decimal Total
        {
            get
            {
                return Items.Sum(item => item.Quantity * item.Product.Price);
            }
        }
    }
}

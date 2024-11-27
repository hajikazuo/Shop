using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.DTO.Basket
{
    public class BasketDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

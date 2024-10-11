using Shop.Common.Models.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.DTO.Product
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public long Stock { get; set; }
        public string? ImageURL { get; set; }
        public CategoryDto? Category { get; set; }
    }
}

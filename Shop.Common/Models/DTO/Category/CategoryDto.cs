using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.DTO.Category
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
    }
}

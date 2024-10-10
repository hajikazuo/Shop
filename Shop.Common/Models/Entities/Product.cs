using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public long Stock { get; set; }

        [StringLength(50)]
        public string? ImageURL { get; set; }

        public Category? Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}

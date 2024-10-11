using AutoMapper;
using Shop.Common.Models.DTO.Category;
using Shop.Common.Models.DTO.Product;
using Shop.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shop.Common.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Product, ProductDto>().ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<ProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}

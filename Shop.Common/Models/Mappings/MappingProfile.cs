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
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<CategoryRequestDto, Category>().ReverseMap();

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<ProductRequestDto, Product>().ReverseMap();
        }
    }
}

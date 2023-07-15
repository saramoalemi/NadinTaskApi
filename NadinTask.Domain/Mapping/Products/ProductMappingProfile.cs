using AutoMapper;
using NadinTask.Domain.DTOs.Products;
using NadinTask.Domain.Models.Products;
using NadinTask.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Mapping.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}

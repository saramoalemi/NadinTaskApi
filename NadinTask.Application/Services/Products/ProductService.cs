using AutoMapper;
using NadinTask.Application.Interfaces.Products;
using NadinTask.Domain.DTOs.Products;
using NadinTask.Domain.Models.Products;
using NadinTask.Domain.ViewModels.Products;
using NadinTask.Infrastructure.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Application.Services.Products
{
    public class ProductService : Base.BaseService<Product, ProductDto, ProductViewModel, long>, IProductService
    {
        public ProductService(IProductRepository repository, IMapper mapper) : base(repository, mapper)
        {

            _mapperConfiguration = new MapperConfiguration(cfg =>
                         cfg.CreateProjection<Product, ProductViewModel>());
        }
    }
}

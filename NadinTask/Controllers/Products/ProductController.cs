using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NadinTask.Application.Interfaces.Products;
using NadinTask.Domain.DTOs.Products;
using NadinTask.Domain.Models.Products;
using NadinTask.Domain.ViewModels.Products;

namespace NadinTask.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :Base.BaseController<Product, ProductDto, ProductViewModel, long>
    {
        private readonly IProductService _service;
        public ProductController(IProductService service) : base(service)
        {
            _service = service;
        }
    }
}

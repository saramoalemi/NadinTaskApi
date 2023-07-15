using NadinTask.Application.Services.Base;
using NadinTask.Domain.DTOs.Products;
using NadinTask.Domain.Models.Products;
using NadinTask.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Application.Interfaces.Products
{
    public interface IProductService : IBaseService<Product, ProductDto, ProductViewModel, long>
    {

    }
}

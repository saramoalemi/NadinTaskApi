using NadinTask.Domain.Models.Products;
using NadinTask.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Infrastructure.Interfaces.Products
{
    public interface IProductRepository : IBaseRepository<Product, long>
    {
    }
}

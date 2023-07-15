using NadinTask.Domain.Models.Products;
using NadinTask.Infrastructure.Interfaces.Products;
using NadinTask.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Infrastructure.Repositories.Products
{
    public class ProductRepository : BaseRepository<Product, long>, IProductRepository
    {
        public ProductRepository(NadinContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

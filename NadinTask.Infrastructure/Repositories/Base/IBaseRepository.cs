
using Microsoft.AspNetCore.Identity;
using NadinTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Infrastructure.Repositories.Base
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable
         where TEntity : ObjectModel<TKey>
          where TKey : struct
    {
        Task<TEntity> GetAsync(TKey key);
        Task<IQueryable<TEntity>> GetAllAsync(bool trackChanges);
         Task<IQueryable<TEntity>> GetListAsync(List<TKey> keys);
        
        Task<long> GetCount();
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> func);
        Task<int> CreateAsync(TEntity entity);
        Task<int> CreateListAsync(List<TEntity> list);
        Task<int> EditAsync(TEntity entity);
        Task<int> EditListAsync(List<TEntity> list);
        Task<int> DeleteAsync(TKey key);
        Task<int> DeleteListAsync(List<TEntity> list);
        Task<int> DeleteListAsync(List<TKey> keys);
        Task<int> UnDeleteAsync(TKey key);
        Task<int> UnDeleteListAsync(List<TEntity> list);
        Task<int> UnDeleteListAsync(List<TKey> keys);
        Task<int> SaveAsync();
    }
}


using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Application.Services.Base
{
    public interface IBaseService<TEntity, TDtoEntity, TViewEntity, TKey> : IDisposable
         where TEntity : Domain.Models.ObjectModel<TKey>
         where TDtoEntity : Domain.DTOs.Base.BaseDto<TKey>
         where TViewEntity : Domain.ViewModels.Base.BaseViewModel<TKey>
          where TKey : struct
    {
        Task<TViewEntity> CreateAsync(TDtoEntity instance);
        Task<TViewEntity> EditAsync(TDtoEntity instance);
        Task<int> DeleteAsync(TKey key);
        Task<int> UnDeleteAsync(TKey key);
        Task<IQueryable<TViewEntity>> CreateListAsync(List<TDtoEntity> list);
        Task<IQueryable<TViewEntity>> EditListAsync(List<TDtoEntity> list);
        Task<int> DeleteListAsync(List<TKey> keys);
        Task<int> UnDeleteListAsync(List<TKey> keys);
        Task<TViewEntity> GetAsync(TKey key);
        Task<IQueryable<TViewEntity>> GetListAsync();
        Task<IEnumerable<TViewEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression);
       // Task<long> GetRecordCount(IQueryable<TViewEntity> list);
        Task<long> GetRecordCount();
    }
}

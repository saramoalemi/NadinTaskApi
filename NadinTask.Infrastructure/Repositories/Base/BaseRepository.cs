
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using NadinTask.Domain.Models;
using NadinTask.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NadinTask.Infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : ObjectModel<TKey>
         where TKey : struct, IEquatable<TKey>
    {
        private bool _disposed;
        protected NadinContext _context;
        protected DbSet<TEntity> EntityCollection => _context.Set<TEntity>();

        public BaseRepository(NadinContext repositoryContext) =>
            _context = repositoryContext;

        #region Before And After
        protected virtual Task BeforeCreate(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task BeforeEdit(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task BeforeDelete(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task BeforeUnDelete(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task BeforePatch(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task AfterCreate(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task AfterEdit(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task AfterDelete(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task AfterUnDelete(TEntity instance)
        {
            return Task.CompletedTask;
        }
        protected virtual Task AfterPatch(TEntity instance)
        {
            return Task.CompletedTask;
        }
        #endregion

        public async Task<int> CreateAsync(TEntity entity)
        {
            await BeforeCreate(entity);
            await EntityCollection.AddAsync(entity);
            if (await SaveAsync() > 0)
                await AfterCreate(entity);
            return await SaveAsync();

        }
        public async Task<int> EditAsync(TEntity entity)
        {
            await BeforeEdit(entity);
            await Task.Run(() => EntityCollection.Update(entity));
            if (await SaveAsync() > 0)
                await AfterEdit(entity);
            return await SaveAsync();
        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            await BeforeDelete(entity);
            if (entity == null)
            {
                //return;
            }
            await Task.Run(() => entity.IsDeleted_ = true);
            if (await SaveAsync() > 0)
                await AfterDelete(entity);
            return await SaveAsync();

        }
        public async Task<int> DeleteAsync(TKey key)
        {
            var entity = await EntityCollection.FirstOrDefaultAsync(x => x.ID.Equals(key));
            if (entity == null)
            {
                // return;
            }
            return await DeleteAsync(entity);
        }

        public async Task<int> UnDeleteAsync(TEntity entity)
        {
            await BeforeUnDelete(entity);

            if (entity == null)
            {
                // return;
            }
            await Task.Run(() => entity.IsDeleted_ = false);
            if (await SaveAsync() > 0)
                await AfterUnDelete(entity);
            return await SaveAsync();


        }
        public async Task<int> UnDeleteAsync(TKey key)
        {
            var entity = await EntityCollection.FirstOrDefaultAsync(x => x.ID.Equals(key));
            if (entity == null)
            {
                // return;
            }
            return await UnDeleteAsync(entity);
        }


        public async Task<TEntity> GetAsync(TKey key)
        {
            return await EntityCollection.FirstAsync(x => x.ID.Equals(key) && !x.IsDeleted_);
        }
        public async Task<IQueryable<TEntity>> GetAllAsync(bool trackChanges)
        {
           
            return !trackChanges ?
                await Task.Run(() => EntityCollection.Where(x => !x.IsDeleted_).AsNoTracking())
                : await Task.Run(() => EntityCollection.Where(x => !x.IsDeleted_));
        }
     
       
        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public async Task<int> CreateListAsync(List<TEntity> list)
        {
            list.ForEach(async x => await BeforeCreate(x));
            await EntityCollection.AddRangeAsync(list);
            if (await SaveAsync() > 0)
                list.ForEach(async x => await AfterCreate(x));
            return await SaveAsync();


        }

        public async Task<int> EditListAsync(List<TEntity> list)
        {
            list.ForEach(async x => await BeforeEdit(x));
            await Task.Run(() => EntityCollection.UpdateRange(list));
            if (await SaveAsync() > 0)
                list.ForEach(async x => await AfterEdit(x));
            return await SaveAsync();

        }

        public async Task<int> DeleteListAsync(List<TEntity> list)
        {
            list.ForEach(async x => await BeforeDelete(x));

            await Task.Run(() => list.ForEach(x => x.IsDeleted_ = true));
            if (await SaveAsync() > 0)
                list.ForEach(async x => await AfterDelete(x));
            return await SaveAsync();

        }
        public async Task<int> DeleteListAsync(List<TKey> keys)
        {
            var list = await EntityCollection.Where(x => keys.Contains(x.ID)).ToListAsync();

            return await DeleteListAsync(list);

        }
        public async Task<int> UnDeleteListAsync(List<TEntity> list)
        {
            list.ForEach(async x => await BeforeUnDelete(x));

            await Task.Run(() => list.ForEach(x => x.IsDeleted_ = false));
            var result = await SaveAsync();
            if (result > 0)
                list.ForEach(async x => await AfterUnDelete(x));
            return await SaveAsync();
        }
        public async Task<int> UnDeleteListAsync(List<TKey> keys)
        {
            var list = await EntityCollection.Where(x => keys.Contains(x.ID)).ToListAsync();

            return await UnDeleteListAsync(list);
        }

        public async Task<IQueryable<TEntity>> GetListAsync(List<TKey> keys)
        {
            return EntityCollection.Where(x => keys.Contains(x.ID));
        }

        public async Task<long> GetCount()
        {
            return await EntityCollection.LongCountAsync(x => !x.IsDeleted_);
        }

       
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> func)
        {

                    return EntityCollection.Where(func);
               
        }
    }
}


using NadinTask.Domain.DTOs.Products;
using NadinTask.Domain.Models.Products;
using NadinTask.Infrastructure.Repositories.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http.Results;

namespace NadinTask.Application.Services.Base
{
    public class BaseService<TEntity, TDtoEntity, TViewEntity, TKey> : IBaseService<TEntity, TDtoEntity, TViewEntity, TKey>
         where TEntity : Domain.Models.ObjectModel<TKey>
         where TDtoEntity : Domain.DTOs.Base.BaseDto<TKey>
         where TViewEntity : Domain.ViewModels.Base.BaseViewModel<TKey>
         where TKey : struct, IEquatable<TKey>
    {
        protected IBaseRepository<TEntity, TKey> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity, TKey> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        protected MapperConfiguration _mapperConfiguration;

        protected virtual Task BeforeCreate(TDtoEntity instance, TEntity entity)
        {
            return Task.CompletedTask;
        }
        protected virtual Task BeforeEdit(TDtoEntity instance, TEntity entity)
        {
            return Task.CompletedTask;
        }
        protected virtual Task BeforeDelete(TKey key)
        {
            return Task.CompletedTask;
        }

        protected virtual Task BeforePatch(TDtoEntity instance, TEntity entity)
        {
            return Task.CompletedTask;
        }
        protected virtual IQueryable<TViewEntity> AfterMap(IQueryable<TViewEntity> result)
        {
            return result;
        }

        public async Task<TViewEntity> CreateAsync(TDtoEntity instance)
        {
            var entity = _mapper.Map<TEntity>(instance);
            await BeforeCreate(instance, entity);
            await _repository.CreateAsync(entity);
            return _mapper.Map<TViewEntity>(entity);
        }
        public async Task<TViewEntity> EditAsync(TDtoEntity instance)
        {
            var entity = await _repository.GetAsync(instance.Id);
            entity = _mapper.Map(instance, entity);
            await BeforeEdit(instance, entity);

            await _repository.EditAsync(entity);
            return _mapper.Map<TViewEntity>(entity);

        }



        public async Task<int> DeleteAsync(TKey key)
        {
            await BeforeDelete(key);
            return await _repository.DeleteAsync(key);


        }
        public async Task<int> UnDeleteAsync(TKey key)
        {
            return await _repository.UnDeleteAsync(key);

        }
        public void Dispose()
        {

        }

        public virtual async Task<TViewEntity> GetAsync(TKey key)
        {
            var instance = await _repository.GetAsync(key);
            return _mapper.Map<TEntity, TViewEntity>(instance);
        }

        public async Task<IQueryable<TViewEntity>> GetListAsync()
        {
            var result = await _repository.GetAllAsync(false);
            return _mapper.ProjectTo<TViewEntity>(result);
        }
        public async Task<IQueryable<TViewEntity>> GetListAsync(int year)
        {
            var result = await _repository.GetAllAsync(false, year);
            return _mapper.ProjectTo<TViewEntity>(result);
        }


        public async Task<IQueryable<TViewEntity>> CreateListAsync(List<TDtoEntity> list)
        {
            var realList = _mapper.ProjectTo<TEntity>(list.AsQueryable());
            await _repository.CreateListAsync(realList.ToList());
            return _mapper.ProjectTo<TViewEntity>(list.AsQueryable());
        }

        public async Task<IQueryable<TViewEntity>> EditListAsync(List<TDtoEntity> list)
        {

            return _mapper.ProjectTo<TViewEntity>(list.AsQueryable());
        }

        public async Task<int> DeleteListAsync(List<TKey> keys)
        {
            return await _repository.DeleteListAsync(keys);
        }

        public async Task<int> UnDeleteListAsync(List<TKey> keys)
        {
            return await _repository.UnDeleteListAsync(keys);
        }

        public async Task<long> GetRecordCount(IQueryable<TViewEntity> list)
        {
            return await list.LongCountAsync();
        }

        public Task<long> GetRecordCount()
        {
            return _repository.GetCount();
        }

        public async Task<IEnumerable<TViewEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = _repository.GetQueryable(expression);
            var vresult = _mapper.ProjectTo<TViewEntity>(result);
            return AfterMap(vresult);
        }


    }
}

using NadinTask.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//using NadinTask.API.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.OpenApi.Extensions;
using System.Reflection;
using NadinTask.Domain.DTOs.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NadinTask.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NadinTask.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TDtoEntity, TViewEntity, TKey> : ControllerBase
         where TEntity : Domain.Models.ObjectModel<TKey>
         where TDtoEntity : Domain.DTOs.Base.BaseDto<TKey>
         where TViewEntity : Domain.ViewModels.Base.BaseViewModel<TKey>
         where TKey : struct, IEquatable<TKey>
    {

        private readonly IBaseService<TEntity, TDtoEntity, TViewEntity, TKey> _baseService;

        public BaseController(IBaseService<TEntity, TDtoEntity, TViewEntity, TKey> baseService)
        {
            _baseService = baseService;
        }



        [HttpPost("Create")]
        public virtual async Task<IActionResult> Create(TDtoEntity instance)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _baseService.CreateAsync(instance);
            if (result != null)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("{id}")]
        public virtual async Task<TViewEntity> Get(TKey id)
        {
            return await _baseService.GetAsync(id);
        }

        [HttpPost("Edit")]
        public virtual async Task<ActionResult> Edit(TDtoEntity instance)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _baseService.EditAsync(instance);
            return result != null ? Ok(result) : BadRequest();
        }



        [HttpDelete("{id}")]
      
        public virtual async Task<ActionResult> Delete(TKey id)
        {
            var result = await _baseService.DeleteAsync(id);
            return result >= 0 ? Ok() : BadRequest();
        }

        [HttpPost("UnDelete")]
      
        public virtual async Task<ActionResult> UnDelete(TKey id)
        {
            var result = await _baseService.UnDeleteAsync(id);
            return result >= 0 ? Ok() : BadRequest();
        }


    }
}

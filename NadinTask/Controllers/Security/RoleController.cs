using NadinTask.Domain.ViewModels.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection;
using Microsoft.VisualBasic;
using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Application.Interfaces.Security;
//using System.Web.Http.Results;
using NadinTask.API.Services;

namespace NadinTask.API.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
   
    public class RoleController : ControllerBase
    {
        private RoleManager<Role> _roleManager;
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService, RoleManager<Role> roleManager)
        {

            _roleService = roleService;
            _roleManager = roleManager;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(RoleDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleService.Create(createDto);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _roleService.Delete(id);
            if (result == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (result.Succeeded)
                return Ok(result);
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        [HttpPost("Edit")]
        public async Task<ActionResult> Edit(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleService.Edit(role);

            if (result == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (result.Succeeded)
                return Ok(result);

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

      

    }
}

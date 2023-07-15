
using NadinTask.Application.Interfaces.Security;
using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Domain.ViewModels.Security;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace NadinTask.API.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromForm]UserRegisterDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _accountService.Register(request);
            if (result.Succeeded)
            {
               
                return Ok("کاربر ثبت نام کرد");
            }
            return BadRequest(result.Errors);

        }

        [HttpPost("SetPassword")]
        public async Task<ActionResult> SetPassword(SetPasswordDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.SetPasswordAsync(request);
            if (request == null)
                return BadRequest();
            if (result.Errors.Count() > 0)
            {
                return BadRequest(result);
            }
            return Ok("گذرواژه کاربر با موفقیت تغییر یافت");
        }

     

        [HttpGet("GetUserById")]
      
        public async Task<ActionResult> GetUserById(int id )
        {
           var user =await  _accountService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("UserList")] 
       
        public async Task<ActionResult> UserList()
        {

            var users = await _accountService.GetUsers();
            return Ok(await users.ToListAsync());
        }

        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
           
            var result = await _accountService.DeleteUserasync(id);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
                return Ok(result); 

        }

        [HttpPost("EditUser")]
        public async Task<ActionResult> EditUser([FromForm]AccountEditDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.UserEditAsync(request);

            if (result.Errors.Count() > 0)
            {
                return BadRequest(result);
            }
            return Ok("تغییرات با موفقیت انجام شد");

  
        }

    }
}

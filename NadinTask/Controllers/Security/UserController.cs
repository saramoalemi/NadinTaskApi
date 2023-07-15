using NadinTask.API.Services;
using NadinTask.Application.Interfaces.Security;
using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.ViewModels.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinTask.Domain.DTOs.Security;
using System.Security.Claims;

namespace NadinTask.API.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {

        //public IUnitOfWork _unitOfWork;
        // define the mapper
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto request)
        {
            HttpContext.Request.Headers.TryGetValue("IpAddress", out var userIp);
            HttpContext.Request.Headers.TryGetValue("MacAddress", out var userMac);
            var result = await _accountService.Login(request, userIp, userMac);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("کلمه عبور و گذر واژه نامعتبر است");
        }


        [HttpPost("EditProfile")]
         public async Task<ActionResult> EditProfile([FromForm] UserEditDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.EditProfileAsync(request);

            if (result.Errors.Count() > 0)
            {
                return BadRequest(result);
            }
            return Ok("تغییرات با موفقیت انجام شد");
        }


        private UserViewModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserViewModel
                {
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                };
            }
            return null;
        }


    }
}


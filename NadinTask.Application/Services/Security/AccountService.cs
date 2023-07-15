using NadinTask.Application.Interfaces.Security;
using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
//using NadinTask.Domain.Static;
//using NadinTask.Domain.Utility;
using NadinTask.Domain.ViewModels;
using NadinTask.Domain.ViewModels.Security;
using NadinTask.Infrastructure.Interfaces.Security;
using NadinTask.Infrastructure.Repositories.Security;
using AutoMapper;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http.Results;
using static System.Net.Mime.MediaTypeNames;


namespace NadinTask.Application.Services.Security
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<UserLoginViewModel> Login(UserLoginDto userLogin, string ip, string mac)
        {

            var user = await _userRepository.LoginAsync(userLogin);
            if (user == null)
            {
                return null;
            }
           
         
            var userViewModel = new UserLoginViewModel()
            {
                Id = user.Id,
                Name = user.Name_User,
                UserName = user.UserName,
                TokenExpiration = DateTime.Now.AddMinutes(50)                    
            };
            userViewModel.Token = await CreateToken(user);
          

            return userViewModel;
        }
   

        public async Task<IEnumerable<string>> GetUserRoles (string userName)
        {
            var user = await _userRepository.GetUserByNameAsync(userName);
           return await _userRepository.GetUserRoles(user);
        }

        public async Task<IdentityResult> Register(UserRegisterDto request)
        {
            try
            {
                var result = await _userRepository.RegisterUserAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserViewModel> GetUserById(int id)
        {
            return _mapper.Map<UserViewModel>(await _userRepository.GetUserByIDAsync(id));
        }
        public async Task<UserViewModel> GetUserByName(string name)
        {
            return _mapper.Map<UserViewModel>(await _userRepository.GetUserByNameAsync(name));
        }
        public async Task<IQueryable<UserViewModel>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.ProjectTo<UserViewModel>(users);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto request)
        {
            return await _userRepository.ChangePasswordAsync(request);
        }
        public async Task<IdentityResult> SetPasswordAsync(SetPasswordDto request)
        {
            return await _userRepository.SetPasswordAsync(request);
        }
        public async Task<IdentityResult> DeleteUserasync(int userId)
        {
            return await _userRepository.Delete(userId);
        }

        public async Task<IdentityResult> UserEditAsync(AccountEditDto request)
        {
            var user = await _userRepository.GetUserByIDAsync(request.id);
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            var usermap = _mapper.Map(request, user);


            return await _userRepository.Edit(usermap);
        }
        public async Task<IdentityResult> EditProfileAsync(UserEditDto request)
        {
            var user = await _userRepository.GetUserByIDAsync(request.id);
            if (user == null)
            {
                return IdentityResult.Failed();
            }

            var usermap = _mapper.Map(request, user);


            return await _userRepository.Edit(usermap);
        }
      

        private async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.GivenName,user.Name_User?? "کاربر")
            };
            if (user.Email != null)
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            if (user.PhoneNumber != null)
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            var roles = await _userRepository.GetUserRoles(user);
            roles.ToList().ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtConfig:secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:validIssuer"],
                audience: _configuration["JwtConfig:validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: creds
                );
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }
    }
}

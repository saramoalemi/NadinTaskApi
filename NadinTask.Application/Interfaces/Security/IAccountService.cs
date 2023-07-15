using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Domain.ViewModels;
using NadinTask.Domain.ViewModels.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Application.Interfaces.Security
{
    public interface IAccountService
    {
        Task<UserLoginViewModel> Login(UserLoginDto userLogin,string ip , string mac);
        Task<IEnumerable<string>> GetUserRoles(string userName);
        Task<IdentityResult> Register(UserRegisterDto request);
        Task<UserViewModel> GetUserById(int id);
        Task<UserViewModel> GetUserByName(string name);
        Task<IQueryable<UserViewModel>> GetUsers();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto request);
        Task<IdentityResult> SetPasswordAsync(SetPasswordDto request);
        Task<IdentityResult> DeleteUserasync(int userId);
        Task<IdentityResult> EditProfileAsync(UserEditDto request);
        Task<IdentityResult> UserEditAsync(AccountEditDto request);


    }
}

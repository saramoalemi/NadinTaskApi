using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Infrastructure.Interfaces.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace NadinTask.Infrastructure.Repositories.Security;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;

    public UserRepository(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
        _roleManager = roleManager;
    }


    public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegistration)
    {
        var role = await _roleManager.FindByNameAsync(userRegistration.Role_User);
        if (!string.IsNullOrEmpty(userRegistration.Role_User) &&  role == null)
        {
            throw new Exception("گروه کاربری مورد نظر یافت نشد");
        }
        var user = _mapper.Map<User>(userRegistration);

        var result =string.IsNullOrEmpty(userRegistration.Password)? await _userManager.CreateAsync(user) : await _userManager.CreateAsync(user, userRegistration.Password);

        if (result.Succeeded)
        {

            await _userManager.AddToRoleAsync(user, userRegistration.Role_User);
        }

        return result;
    }

    public async Task<User?> LoginAsync(UserLoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return null;
        }


        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result)
            user.AccessFailedCount++;
        else
        {
            if (!user.IsActive_)
            {
                throw new UnauthorizedAccessException("کاربر شما غیر فعال شده است.");
            }

            user.AccessFailedCount = 0;
        }
        await _userManager.UpdateAsync(user);
        return result ? user : null;
    }

    public async Task<User> GetUserByNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name);
    }
    public async Task<User> GetUserByIDAsync(int id)
    {
        return _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
    }
    public async Task<IQueryable<User>> GetUsersAsync()
    {
        return _userManager.Users;
    }
    public async Task<IQueryable<User>> GetUsersAsync(Expression<Func<User, bool>> expression)
    {
        return _userManager.Users.Where(expression).AsQueryable();
    }
 


    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            return await Task.FromResult(IdentityResult.Failed(new IdentityError()
            { Code = "Failed", Description = "نام کاربری یا کلمه عبور نامعتبر است" }));
        }

        var hashed = user.PasswordHash;
        if (request.NewPassword != request.ConfirmPassword)
            throw new Exception();
        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (result.Succeeded)
        {
          
            return await _userManager.UpdateAsync(user);
        }
        return result;


        //return null;
    }

    public async Task<IdentityResult> SetPasswordAsync(SetPasswordDto request)
    {
        var user = await GetUserByIDAsync(request.UserId);
        if (user == null)
        {
            // return null;
        }
        await _userManager.RemovePasswordAsync(user);
        var result = await _userManager.AddPasswordAsync(user, request.Password);
        return result;
    }
    public async Task<IdentityResult> Edit(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result;
    }
    public async Task<IdentityResult> Delete(int userId)
    {
        var user = await GetUserByIDAsync(userId);
        if (user == null)
        {
            // return null;
        }
        user.IsDeleted_ = true;
        var result = await _userManager.UpdateAsync(user);
        return result;
    }

    public async Task<IdentityResult> UnDelete(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.IsDeleted_ && x.UserName == userName);
        if (user == null)
        {
            // return null;
        }
        user.IsDeleted_ = false;
        var result = await _userManager.UpdateAsync(user);
        return result;
    }
    public async Task<IdentityResult> AddUserToRole(int userID, string roleName)
    {
        var user = await GetUserByIDAsync(userID);
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result;
    }
    public async Task<IdentityResult> RemoveUserFromRole(int userID, string roleName)
    {
        var user = await GetUserByIDAsync(userID);
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        return result;
    }


    public async Task<IEnumerable<string>> GetUserRoles(int userID)
    {
        var user = await GetUserByIDAsync(userID);
        var result = await _userManager.GetRolesAsync(user);
        return result;
    }
    public async Task<IEnumerable<string>> GetUserRoles(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

   
}

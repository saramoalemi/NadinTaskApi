using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;

namespace NadinTask.Infrastructure.Interfaces.Security;

public interface IUserRepository
{
    Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegistration);
    Task<User> LoginAsync(UserLoginDto loginDto);
    Task<User> GetUserByNameAsync(string name);
    Task<User> GetUserByIDAsync(int id);
    Task<IQueryable<User>> GetUsersAsync();
    Task<IQueryable<User>> GetUsersAsync(Expression<Func<User, bool>> expression);
    Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordRequest);
    Task<IdentityResult> SetPasswordAsync(SetPasswordDto setPasswordRequest);
    Task<IdentityResult> Edit(User user);
    Task<IdentityResult> Delete(int userId);
    Task<IdentityResult> UnDelete(string userName);
    Task<IdentityResult> AddUserToRole(int userID, string roleName);
    Task<IdentityResult> RemoveUserFromRole(int userID, string roleName);
    Task<IEnumerable<string>> GetUserRoles(User user);

}


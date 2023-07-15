using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Domain.ViewModels.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Application.Interfaces.Security
{
    public interface IRoleService
    {
        Task<IdentityResult> Create(RoleDto createDto);
        Task<IdentityResult> Edit(RoleDto editDto);
        Task<IdentityResult> Delete(int id);
        Task<RoleViewModel> FindByIdAsync(int id);
        Task<IEnumerable<string>> RolePermissions(string roleName);
        Task<List<IdentityResult>> SaveUsersInRole(int roleId, int[] userIds);
        Task<IEnumerable<RoleViewModel>> GetList();
    }
}

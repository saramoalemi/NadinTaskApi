using NadinTask.Domain.Models.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Infrastructure.Interfaces.Security
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetListWithPermissions();
        Task<IEnumerable<Role>> GetListAsync();
        Task<Role> GetRoleById(int id);
        Task<IEnumerable<string>> GetRolePermissions(string roleName);
        Task<IdentityResult> Edit(Role role);
        Task<IdentityResult> Delete(int roleId);
        Task<IdentityResult> UnDelete(string roleName);
        Task<IdentityResult> Create(Role role);
        Task<IQueryable<User>> GetUsersInRole(string roleName);
        Task<List<IdentityResult>> SetUsersInRole(int roleId , int[]userIds);
        IEnumerable<int> GetUserIds(int id);

    }


}

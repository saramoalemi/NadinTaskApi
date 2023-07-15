using NadinTask.Domain.Models.Security;
using NadinTask.Infrastructure.Interfaces.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Infrastructure.Repositories.Security
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly NadinContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RoleRepository(NadinContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<Role>> GetListWithPermissions()
        {
            return await _context.Roles.Include(x => x.Role_Permission_List).ToListAsync();
        }
        public async Task<IEnumerable<Role>> GetListAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public IEnumerable<int> GetUserIds(int id)
        {
            List<int> itemsList = new List<int>();
            //string adminID = new User.UserRepository().GetIDByUserName("admin");
            itemsList = _context.Roles
                .Where(x => x.Id == id && !x.IsDeleted_)
                .SelectMany(x => x.Role_User_List)
                .Select(x => x.User.Id).Distinct().ToList();
            //itemsList.Add(adminID);
            return itemsList;
        }
        public Task<IQueryable<User>> GetUsersInRole(string roleName)
        {
            return Task.Run(() => _context.Roles.Where(x => x.Name == roleName && !x.IsDeleted_)
                 .SelectMany(x => x.Role_User_List).Select(x => x.User));
        }

        public async Task<IEnumerable<string>> GetRolePermissions(string roleName)
        {
            var role = await _roleManager.Roles.Include(x => x.Role_Permission_List).FirstOrDefaultAsync(x => x.Name == roleName);
            if (role == null)
            {
              //  return null;

            }
            return role.Role_Permission_List.Select(x => x.PermissionName).ToList();
        }

        public async Task<IdentityResult> Create(Role role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }
        public async Task<IdentityResult> Edit(Role role)
        {
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }
        public async Task<IdentityResult> Delete(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
               // return null;
            }
            role.IsDeleted_ = true;
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }
        public async Task<IdentityResult> UnDelete(string roleName)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.IsDeleted_ && x.Name == roleName);
            if (role == null)
            {
              //  return null;
            }
            role.IsDeleted_ = false;
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }

        public async Task<List<IdentityResult>> SetUsersInRole(int roleId, int[] userIds)
        {
            List<IdentityResult> result = new List<IdentityResult>();
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var users =await _userManager.GetUsersInRoleAsync(role.Name);
            var removeUsers = users.Where(x => !userIds.Contains(x.Id)).ToList();
            var addedUserIds = userIds.Where(x => !users.Select(x => x.Id).Contains(x)).ToList();
            var addedUsers = _context.Users.Where(w => addedUserIds.Contains(w.Id));
            foreach (var user in removeUsers)
            {
                result.Add(await _userManager.RemoveFromRoleAsync(user, role.Name));
            }
            foreach (var user in addedUsers)
            {
                result.Add(await _userManager.AddToRoleAsync(user, role.Name));
            }
            return result;
        }

        
    }
}

using NadinTask.Application.Interfaces.Security;
using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Domain.ViewModels.Security;
using NadinTask.Infrastructure.Interfaces.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Application.Services.Security
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<IdentityResult> Create(RoleDto createDto)
        {
            return await _roleRepository.Create(_mapper.Map<Role>(createDto));
        }
        public async Task<IdentityResult> Edit(RoleDto editDto)
        {
            return await _roleRepository.Edit(_mapper.Map<Role>(editDto));
        }
        public async Task<IdentityResult> Delete(RoleDto role)
        {
            return await _roleRepository.Delete(role.Id);
        }
        public async Task<RoleViewModel> FindByIdAsync(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<IEnumerable<string>> RolePermissions(string roleName)
        {
            var list = await _roleRepository.GetRolePermissions(roleName);
            if (list == null)
                return new List<string>();
            return list;
        }

        public async Task<IEnumerable<RoleViewModel>> GetList()
        {
            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(await _roleRepository.GetListAsync());
        }

        public async Task<IdentityResult> Delete(int id)
        {
            return await _roleRepository.Delete(id);
        }

        public async Task<List<IdentityResult>> SaveUsersInRole(int roleId, int[] userIds)
        {
            return await _roleRepository.SetUsersInRole(roleId, userIds); ;
        }
    }
}

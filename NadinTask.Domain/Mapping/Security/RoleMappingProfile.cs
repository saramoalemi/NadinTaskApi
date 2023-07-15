using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Domain.ViewModels.Security;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArvinERP.Domain.Mapping.Security
{
    public class RoleMappingProfile:Profile 
    {
        public RoleMappingProfile()
        {
           
            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleDto, Role>();

        }
    }
}

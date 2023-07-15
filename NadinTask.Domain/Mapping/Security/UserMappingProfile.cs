using NadinTask.Domain.DTOs.Security;
using NadinTask.Domain.Models.Security;
using NadinTask.Domain.ViewModels.Security;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Mapping.Security
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            //CreateMap<UserRegisterDto, User>().ReverseMap();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserEditDto, User>()
                .ReverseMap();

            CreateMap<AccountEditDto, User>()
               .ReverseMap();

            CreateMap<User, UserViewModel>().ForMember(v => v.Name, u => u.MapFrom(src => src.Name_User));

        }
    }
}

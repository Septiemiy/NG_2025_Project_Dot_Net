using AutoMapper;
using DAL_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationBL.Models;

namespace UserRegistrationBL.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}

using AutoMapper;
using DAL_Core.Entities;
using DeviceGatewayBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Profiles
{
    public class TriggerMapperProfile : Profile
    {
        public TriggerMapperProfile()
        {
            CreateMap<TriggerDTO, Trigger>()
                .ReverseMap();
        }
    }
}

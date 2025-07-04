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
    public class ThresholdMapperProfile : Profile
    {
        public ThresholdMapperProfile() 
        {
            CreateMap<ThresholdDTO, Threshold>()
                .ReverseMap();
        }
    }
}

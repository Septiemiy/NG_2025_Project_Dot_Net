using AutoMapper;
using DAL_Core.Entities;
using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using DeviceGatewayDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services
{
    public class ThresholdService : IThresholdService
    {
        private readonly IThresholdRepository _thresholdRepository;
        private readonly IMapper _mapper;
        public ThresholdService(IThresholdRepository thresholdRepository, IMapper mapper)
        {
            _thresholdRepository = thresholdRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddThresholdAsync(ThresholdDTO thresholdDTO)
        {
            var threshold = _mapper.Map<Threshold>(thresholdDTO);

            await _thresholdRepository.CreateAsync(threshold);

            return threshold.Id;
        }
    }
}

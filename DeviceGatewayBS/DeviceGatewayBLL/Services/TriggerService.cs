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
    public class TriggerService : ITriggerService
    {
        private readonly ITriggerRepository _triggerRepository;
        private readonly IMapper _mapper;

        public TriggerService(ITriggerRepository triggerRepository, IMapper mapper)
        {
            _triggerRepository = triggerRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddTriggerAsync(TriggerDTO triggerDTO)
        {
            var trigger = _mapper.Map<Trigger>(triggerDTO);

            await _triggerRepository.CreateAsync(trigger);

            return trigger.Id;
        }
    }
}

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
    public class TelemetryService : ITelemetryService
    {
        private readonly ITelemetryRepository _telemetryRepository;
        private readonly IMapper _mapper;

        public TelemetryService(ITelemetryRepository telemetryRepository, IMapper mapper)
        {
            _telemetryRepository = telemetryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddTelemetryAsync(TelemetryDTO telemetryDTO)
        {
            var telemetry = _mapper.Map<Telemetry>(telemetryDTO);

            await _telemetryRepository.CreateAsync(telemetry);

            return telemetry.Id;
        }
    }
}

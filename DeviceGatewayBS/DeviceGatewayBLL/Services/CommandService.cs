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
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandService(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddCommandAsync(CommandDTO commandDTO)
        {
            var command = _mapper.Map<Command>(commandDTO);

            await _commandRepository.CreateAsync(command);

            return command.Id;
        }
    }
}

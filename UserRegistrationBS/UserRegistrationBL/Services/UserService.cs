using AutoMapper;
using DAL_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationBL.Models;
using UserRegistrationBL.Services.Interfaces;
using UserRegistrationDAL.Repositories.Interface;

namespace UserRegistrationBL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userRepository.CreateAsync(user);
            
            return user.Id;
        }
    }
}

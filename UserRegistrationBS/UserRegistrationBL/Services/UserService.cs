using AutoMapper;
using DAL_Core.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenProviderService _jwtTokenProviderService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtTokenProviderService jwtTokenProviderService , IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenProviderService = jwtTokenProviderService;
            _mapper = mapper;
        }

        public async Task<string> CheckUserLogin(UserLoginDTO userLoginDto)
        {
            var user = await _userRepository.GetUserByUsername(userLoginDto.Username);

            if (user == null)
            {
                return String.Empty;
            }
            else
            {
                var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);
                if (verifyResult == PasswordVerificationResult.Success)
                {
                    var token = _jwtTokenProviderService.GenerateToken(user);
                    return token;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public async Task<string> CreateUserAsync(UserRegisterDTO userRegisterDto)
        {
            userRegisterDto.Password = _passwordHasher.HashPassword(null, userRegisterDto.Password);
            var user = _mapper.Map<User>(userRegisterDto);

            await _userRepository.CreateAsync(user);

            var token = _jwtTokenProviderService.GenerateToken(user);

            return token;
        }
    }
}

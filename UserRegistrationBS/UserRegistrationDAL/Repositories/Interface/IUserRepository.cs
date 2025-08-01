﻿using DAL_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationDAL.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> IsUserByUsernameAndEmailExistAsync(string username, string email);
    }
}

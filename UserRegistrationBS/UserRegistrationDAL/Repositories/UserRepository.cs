using DAL_Core;
using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationDAL.Repositories.Interface;

namespace UserRegistrationDAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SmartHomeDbContext _ctx;
        public UserRepository(SmartHomeDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _ctx.Set<User>()
                .FirstOrDefaultAsync(u => u.Username == username);

            return user;
        }
    }
}

using DAL_Core;
using DAL_Core.Entities;
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
    }
}

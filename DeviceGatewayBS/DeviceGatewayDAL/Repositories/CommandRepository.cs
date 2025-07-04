using DAL_Core;
using DAL_Core.Entities;
using DeviceGatewayDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayDAL.Repositories
{
    public class CommandRepository : Repository<Command>, ICommandRepository
    {
        private readonly SmartHomeDbContext _ctx;
        public CommandRepository(SmartHomeDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}

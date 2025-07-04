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
    public class TriggerRepository : Repository<Trigger>, ITriggerRepository
    {
        private readonly SmartHomeDbContext _ctx;
        public TriggerRepository(SmartHomeDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}

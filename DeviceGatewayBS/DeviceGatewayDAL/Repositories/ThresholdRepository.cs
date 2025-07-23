using DAL_Core;
using DAL_Core.Entities;
using DeviceGatewayDAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayDAL.Repositories
{
    public class ThresholdRepository : Repository<Threshold>, IThresholdRepository
    {
        private readonly SmartHomeDbContext _ctx;
        public ThresholdRepository(SmartHomeDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Threshold> GetThresholdByConditionAndValueAsync(string condition, string value)
        {
            return await _ctx.Thresholds
                .Where(t => t.ThresholdCondition == condition && t.Value == value)
                .FirstOrDefaultAsync();
        }
    }
}

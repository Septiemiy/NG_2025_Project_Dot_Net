using DAL_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationBL.Services.Interfaces
{
    public interface IJwtTokenProviderService
    {
        string GenerateToken(User user);
    }
}

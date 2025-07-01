using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Core
{
    public class SmartHomeDbContextFactory : IDesignTimeDbContextFactory<SmartHomeDbContext>
    {
        public SmartHomeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SmartHomeDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-A8GVK05;Database=SmartHome;Trusted_Connection=True;Encrypt=False;");
            return new SmartHomeDbContext(optionsBuilder.Options);
        }
    }
}

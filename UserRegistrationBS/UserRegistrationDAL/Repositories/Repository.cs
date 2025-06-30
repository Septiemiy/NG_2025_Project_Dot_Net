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
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SmartHomeDbContext _ctx;

        public Repository(SmartHomeDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
            await _ctx.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _ctx.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return;
            }

            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public Task<Guid> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

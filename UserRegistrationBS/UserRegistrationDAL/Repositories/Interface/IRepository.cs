using DAL_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationDAL.Repositories.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
        Task<Guid> CreateAsync(T entity);
        Task<Guid> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}

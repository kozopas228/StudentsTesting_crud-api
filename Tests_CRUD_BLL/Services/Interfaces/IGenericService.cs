using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface IGenericService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> DeleteAsync(Guid id);
        public Task<bool> UpdateAsync(T obj);
        public Task<Guid> CreateAsync(T obj);
    }
}
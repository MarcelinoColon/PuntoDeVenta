using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface IReadUseCase<TDTO, TEntity>
    {
        public Task<TDTO> GetByIdAsync(int id);
        public Task<IEnumerable<TDTO>> GetAllAsync();
    }
}

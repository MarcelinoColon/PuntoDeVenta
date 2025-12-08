using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface ICreateRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
    }
}

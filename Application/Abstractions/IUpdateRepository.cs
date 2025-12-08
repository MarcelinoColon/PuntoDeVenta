using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface IUpdateRepository<TEntity>
    {
        Task UpdateAsync(TEntity entity, int id);
    }
}

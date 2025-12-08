using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface IUpdateUseCase<TDTO, TEntity>
    {
        Task UpdateAsync(TDTO dto,int id);
    }
}

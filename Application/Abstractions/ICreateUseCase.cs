using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface ICreateUseCase<TDTO, TEntity>
    {
        Task AddAsync(TDTO dto);
    }
}

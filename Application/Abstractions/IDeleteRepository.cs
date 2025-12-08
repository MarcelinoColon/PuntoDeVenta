using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface IDeleteRepository
    {
        Task DeleteAsync(int id);
    }
}

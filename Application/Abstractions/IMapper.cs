using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn input);
    }
}

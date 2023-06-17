﻿using Arbolus.Model.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arbolus.Model.Interface
{
    public interface IData<T>
    {
        Task<T> Get(string dataType);
    }
}

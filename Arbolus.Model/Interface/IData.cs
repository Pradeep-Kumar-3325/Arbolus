using Arbolus.Model.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arbolus.Model.Interface
{
    // I :- Interface Segregation Principle in SOLID
    public interface IData<T>
    {
        Task<T> Get(string dataType);
    }
}

using Arbolus.Model.Concrete;
using System.Threading.Tasks;

namespace Arbolus.Model.Interface
{
    // Interface Segregation Principle in SOLID
    public interface IRateData
    {
        Task<Rate> Get();
    }
}

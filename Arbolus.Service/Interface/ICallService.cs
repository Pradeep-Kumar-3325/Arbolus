using System.Threading.Tasks;

namespace Arbolus.Service.Interface
{
    public interface ICallService<T>
    {
        // Get Prices for all Experts and Client
        Task<T> GetPrices();

        // Get Prices for specific Expert and Client
        Task<T> GetPriceByName();

    }
}

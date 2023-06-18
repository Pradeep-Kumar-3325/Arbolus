using Arbolus.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arbolus.Service.Interface
{
    public interface ICallService
    {
        // Get Prices for all Experts and Client
        Task<IEnumerable<CallPriceDetails>> GetPrices();
    }
}

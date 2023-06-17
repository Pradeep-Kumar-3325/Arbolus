using Arbolus.Model.Concrete;
using Arbolus.Model.Interface;
using Arbolus.Service.Interface;
using System.Threading.Tasks;

namespace Arbolus.Service
{
    public class CallService<T> : ICallService<T>
    {
        public CallService(IRateData rateData, IData<Expert> expertDetails, IData<Client> clientDetails)
        {

        }

        Task<T> ICallService<T>.GetPrices()
        {
            throw new System.NotImplementedException();
        }

        Task<T> ICallService<T>.GetPriceByName()
        {
            throw new System.NotImplementedException();
        }
    }
}

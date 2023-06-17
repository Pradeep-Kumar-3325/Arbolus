using Arbolus.Model;
using Arbolus.Model.Concrete;
using Arbolus.Model.Interface;
using Arbolus.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arbolus.Service
{
    public class CallService : ICallService
    {
        private readonly ILogger<CallService> logger;

        private readonly IRateData rateData;

        private readonly IData<Expert> expertDetails;

        private readonly IData<Client> clientDetails;

        public CallService(ILogger<CallService> logger, IRateData rateData, IData<Expert> expertDetails, IData<Client> clientDetails)
        {
            this.logger = logger;
            this.rateData = rateData;
            this.expertDetails = expertDetails;
            this.clientDetails = clientDetails;
        }

        public async Task<IEnumerable<CallPriceDetails>> GetPrices()
        {
            try
            {
                throw new System.NotImplementedException();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception in GetPrices while processing data is {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<CallPriceDetails>> GetPriceByName(string expert, string client)
        {
            try
            {
                if (string.IsNullOrEmpty(expert) && string.IsNullOrEmpty(client))
                    throw new Exception("Please provide either expert or client");

                return new List<CallPriceDetails>();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception in GetPriceByName while processing data for expert { expert } and client {client} is {ex}");
                throw;
            }
        }
    }
}

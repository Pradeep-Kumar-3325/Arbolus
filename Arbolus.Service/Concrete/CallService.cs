using Arbolus.Data;
using Arbolus.Model;
using Arbolus.Model.Concrete;
using Arbolus.Model.DTO;
using Arbolus.Model.Interface;
using Arbolus.Service.Base;
using Arbolus.Service.Concrete;
using Arbolus.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbolus.Service
{
    // S :- Single Responsibility of SOLID
    public class CallService : ICallService
    {
        private readonly ILogger logger;

        private readonly IRateData rateData;

        private readonly IData<ExpertData> expertDetails;

        private readonly IData<ClientData> clientDetails;

        private readonly IConfiguration configuration;

        // D :- Dependency Inversion  of SOLID
        public CallService(ILogger<CallService> logger, IRateData rateData, 
            IData<ExpertData> expertDetails, IData<ClientData> clientDetails, IConfiguration configuration)
        {
            this.logger = logger;
            this.rateData = rateData;
            this.expertDetails = expertDetails;
            this.clientDetails = clientDetails;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<CallPriceDetails>> GetPrices()
        {
            try
            {
                await GetData();
                List<CallPriceDetails> callPriceDetails = new List<CallPriceDetails>();

                foreach (Expert expert in Data<ExpertData>.data.Experts)
                {
                    // We can use Parallel for or Parallel Foreach with threadsafe datastructure like concurrency collection to improve preformance 
                    foreach (Call call in expert.Calls)
                    {
                        var destionationCurrency = (Model.Enums.CurrencyEnum)Enum.Parse(typeof(Model.Enums.CurrencyEnum), expert.currency);
                        var rate = Currency.Convert(Model.Enums.CurrencyEnum.USD, destionationCurrency, expert.hourlyRate);
                        var client = Data<ClientData>.data.Clients.Where(x => x.Name.ToLower() == call.client.ToLower()).FirstOrDefault();
                        decimal price = 0m;

                        if (client != null)
                        {
                            if (client.Discounts != null)
                            {
                                /* We can use Parallel for or Parallel Foreach with 
                                 * threadsafe datastructure like concurrency collection to improve preformance 
                                */
                                foreach(var clientDiscount in client.Discounts)
                                {
                                    Discount discount = null;
                                    // O  :- Implement open closed principle of SOLID
                                    switch (clientDiscount)
                                    {
                                        case "FollowUp":
                                            discount = new FollowUp(logger, configuration);
                                            price = discount.GetPrice(call.Duration, rate);
                                            break;
                                        case "1 hour agreement":
                                            discount = new HourAgreement(logger, configuration);
                                            price = discount.GetPrice(call.Duration, rate);
                                            break;
                                        default:
                                            price = GetDefaultPrice(call.Duration, rate);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                price = GetDefaultPrice(call.Duration, rate);
                            }
                        }
                        else
                        {
                            price = GetDefaultPrice(call.Duration, rate);
                        }

                        callPriceDetails.Add(
                            new CallPriceDetails
                            {
                                Expert = expert.Name,
                                Client = call.client,
                                Duration = call.Duration,
                                Price = price
                            }
                            );
                    }
                }

                return callPriceDetails;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception in GetPrices while processing data is {ex}");
                throw;
            }
        }

        public async Task GetData()
        {
            await this.expertDetails.Get("Expert");
            await this.clientDetails.Get("Client");
            await this.rateData.Get();
        }

        public decimal GetDefaultPrice(int duration, decimal rate)
        {
            Discount discount = new Discount(this.logger, this.configuration);
            return discount.GetPrice(duration, rate);
        }
    }
}

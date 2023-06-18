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

        private ExpertData experts;

        private ClientData clients;

        private Rate rates;

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
                Discount discount = null;
                DiscountCreator discountCreator = new DiscountCreator(this.logger, this.configuration);

                foreach (Expert expert in this.experts.Experts)
                {
                    /* DUE TO TIME CRUNCH I DID NOT USE PARALLEL LIBRARY
                     * We can use Parallel for or Parallel Foreach with threadsafe 
                     * datastructure like concurrency collection to improve preformance 
                     */
                    Dictionary<string, int> expertClients = new Dictionary<string, int>();
                    foreach (Call call in expert.Calls)
                    {
                        var destionationCurrency = (Model.Enums.CurrencyEnum)Enum.Parse(typeof(Model.Enums.CurrencyEnum), expert.currency);
                        var rate = Currency.Convert(Model.Enums.CurrencyEnum.USD, destionationCurrency, expert.hourlyRate, this.rates);
                        var client = this.clients.Clients.Where(x => x.Name.ToLower() == call.client.ToLower()).FirstOrDefault();
                        decimal price = 0m;

                        if (client != null)
                        {
                            if (client.Discounts != null)
                            {
                                /* DUE TO TIME CRUNCH I DID NOT USE PARALLEL LIBRARY
                                 * We can use Parallel for or Parallel Foreach with 
                                 * threadsafe datastructure like concurrency collection to improve preformance 
                                */
                                foreach (var clientDiscount in client.Discounts)
                                {
                                    /* O  :- Implement open closed principle of SOLID : 
                                     * Dont need to change untill the below lines has bugs
                                     * For new discount, can extend discount class by creating new drive class
                                     * and update discountcreator
                                     */
                                    if (!discountCreator.Discounts.TryGetValue(clientDiscount.Trim().Replace(" ", "").ToUpper(), out discount))
                                    {
                                        discount = discountCreator.Discounts["DEFAULT"];
                                    }

                                    price += discount.GetPrice(call.Duration, rate, expertClients.ContainsKey(call.client));
                                }
                            }
                            else
                            {
                                price += discountCreator.Discounts["DEFAULT"].GetPrice(call.Duration, rate, true);
                            }
                        }
                        else
                        {
                            price += discountCreator.Discounts["DEFAULT"].GetPrice(call.Duration, rate, true);
                        }

                        callPriceDetails.Add(
                            new CallPriceDetails
                            {
                                Expert = expert.Name,
                                Client = call.client,
                                Duration = call.Duration,
                                Price = Math.Round(price, 2),
                                Currency = expert.currency
                            }
                            );

                        if (!expertClients.ContainsKey(call.client))
                            expertClients.Add(call.client, 1);
                        else
                            expertClients[call.client]++;
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
            this.experts = await this.expertDetails.Get("Expert");
            this.clients = await this.clientDetails.Get("Client");
            this.rates = await this.rateData.Get();
        }
    }
}

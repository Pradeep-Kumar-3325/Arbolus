using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arbolus.Model.Concrete;
using Arbolus.Model.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Arbolus.Data
{
    public class RateData : IRateData
    {
        public Rate rates;

        private readonly ILogger<RateData> logger;

        private readonly IApiService apiService;

        private readonly IConfiguration configuration;

        public RateData(ILogger<RateData> logger, IApiService apiService, IConfiguration configuration)
        {
            this.logger = logger;
            this.apiService = apiService;
            this.configuration = configuration;
        }

        public async Task<Rate> Get()
        {
            try
            {
                this.apiService.SvcUrl = this.configuration["EndPoints:BaseUrl"];
                this.apiService.RequestUri = this.configuration[$"EndPoints:Rate"];
                var result = await this.apiService.Get();
                rates = JsonConvert.DeserializeObject<Rate>(result);
                return rates;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception in Get while processing data for Rate is {ex}");
                throw;
            }
        }
    }
}

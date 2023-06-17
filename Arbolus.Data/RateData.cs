using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arbolus.Model.Concrete;
using Arbolus.Model.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Arbolus.Data
{
     public class RateData : IRateData
    {
        private Rate rates;

        private readonly IApiService apiService;

        private readonly IConfiguration configuration;

        public RateData(IApiService apiService, IConfiguration configuration)
        {
            this.apiService = apiService;
            this.configuration = configuration;
        }

        public async Task<Rate> Get()
        {
            this.apiService.SvcUrl = this.configuration["EndPoints:BaseUrl"];
            this.apiService.RequestUri = this.configuration[$"EndPoints:Rate"];
            var result = await this.apiService.Get();
            rates = JsonConvert.DeserializeObject<Rate>(result);
            return rates;
        }
    }
}

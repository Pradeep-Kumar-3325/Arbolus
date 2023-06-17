using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arbolus.Model.Concrete;
using Arbolus.Model.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Arbolus.Data
{
    public class Data<T> : IData<T>
    {
        private List<T> clients;

        private readonly IApiService apiService;

        private readonly IConfiguration configuration;

        public Data(IApiService apiService, IConfiguration configuration)
        {
            this.apiService = apiService;
            this.configuration = configuration;
        }

        public async Task<List<T>> Get(string dataType)
        {
            this.apiService.SvcUrl = this.configuration["EndPoints:BaseUrl"];
            this.apiService.RequestUri = this.configuration[$"EndPoints:{dataType}"];
            var result = await this.apiService.Get();
            clients = JsonConvert.DeserializeObject<List<T>>(result);
            return clients;
        }
    }
}

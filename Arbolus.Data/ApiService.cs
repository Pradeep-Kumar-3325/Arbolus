using Arbolus.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arbolus.Data
{
    public class ApiService : IApiService
    {
        private HttpClient httpClient;

        public string SvcUrl { get; set; }

        public string RequestUri { get; set; }

        private HttpClient client
        {
            get
            {
                this.httpClient = new HttpClient();
                this.httpClient.BaseAddress = new Uri(this.SvcUrl);
                return this.client;
            }
        }

        public async Task<string> Get()
        {
            try
            {
                var result = await this.httpClient.GetAsync(this.RequestUri);
                return result.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                throw;
            }
        }
        
    }
}

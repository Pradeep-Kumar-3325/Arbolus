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

        public string SvcUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RequestUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<string> Get()
        {
            throw new NotImplementedException();
        }
        
    }
}

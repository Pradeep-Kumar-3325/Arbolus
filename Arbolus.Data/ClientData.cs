using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arbolus.Model.Concrete;
using Arbolus.Model.Interface;

namespace Arbolus.Data
{
     public class ClientData : IClientData
    {
        private static List<Client> clients;
        
        public async Task<List<Client>> Get()
        {
            return clients;
        }
    }
}

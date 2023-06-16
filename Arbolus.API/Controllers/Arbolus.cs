using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arbolus.Model;

namespace Arbolus.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Arbolus : ControllerBase
    {
        private readonly ILogger<Arbolus> _logger;

        public Arbolus(ILogger<Arbolus> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CallPriceDetails> GetAllCallPrice()
        {
            return new List<CallPriceDetails>();
        }
    }
}

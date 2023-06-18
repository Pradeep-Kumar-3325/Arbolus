using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbolus.Service.Base
{
    // discount is abstract because it should be inherited and have some default behaviour or implementation
    public class Discount
    {
        private readonly ILogger logger;

        private readonly IConfiguration configuration;

        public Discount(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public virtual int GracePeriod(int duration)
        {
            try
            {
                var gracePeriod = configuration["Discount:GracePeriod"];
                 if(string.IsNullOrEmpty(gracePeriod))
                    logger.LogInformation($"Configuration of GracePeriod is missing");

                return duration - Convert.ToInt32(gracePeriod);
            }
            catch(Exception ex)
            {
                logger.LogError($"Exception in GracePeriod for duration {duration} is {ex}");
                throw;
            }
        }

        public virtual decimal GetPrice(int duration, decimal rate, bool discountApplicable = true)
        {
            duration = GracePeriod(duration);
            decimal price = (rate / 60) * duration;
            return price;
        }
    }
}

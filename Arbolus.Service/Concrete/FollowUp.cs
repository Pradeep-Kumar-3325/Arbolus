using Arbolus.Service.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Arbolus.Service.Concrete
{
    // S :- Single Responsibility of SOLID
    // L :-  Liskov Substitution Principle  of SOLID
    public class FollowUp :Discount
    {
        private readonly ILogger logger;

        private readonly IConfiguration configuration;

        public FollowUp(ILogger logger, IConfiguration configuration) : base(logger,configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public override decimal GetPrice(int duration, decimal rate, bool discountApplicable = true)
        {
            try
            {
                duration = base.GracePeriod(duration);
                decimal price = (rate / 60) * duration;
                if (discountApplicable)
                {
                    
                    var percentage = configuration["Discount:FollowUp"];
                    if (string.IsNullOrEmpty(percentage))
                        logger.LogInformation($"Configuration of FollowUp is missing");
                    decimal percent = Convert.ToDecimal(percentage);
                    var finalPrice = rate * (percent / 100);
                    return finalPrice;
                }
                else
                    return price;
            }
            catch(Exception ex)
            {
                this.logger.LogError($"Exception in GetPrice of FollowUp while processing data for Rate {rate} and duration {duration}  is {ex}");
                throw;
            }
        }

    }
}

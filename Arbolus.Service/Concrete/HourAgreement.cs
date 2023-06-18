using Arbolus.Model.Concrete;
using Arbolus.Service.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbolus.Service.Concrete
{
    public class HourAgreement : Discount
    {
        private readonly ILogger logger;

        private readonly IConfiguration configuration;

        public HourAgreement(ILogger logger, IConfiguration configuration) : base(logger,configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public override decimal GetPrice(int duration, decimal rate)
        {
            try
            {
                duration = base.GracePeriod(duration);
                var Range = configuration["Discount:HourAgreement"];
                if (string.IsNullOrEmpty(Range))
                    logger.LogInformation($"Configuration of HourAgreement is missing");
                var hourAgreement = JsonConvert.DeserializeObject<HourAgreementRange>(Range);
                decimal price = (rate / 60) * duration;

                if (hourAgreement.MinMinute >= duration && hourAgreement.MaxMinute <= duration)
                {
                    price = (rate / 60) * hourAgreement.ChargedMinute;
                }

                return price;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception in GetPrice of HourAgreement while processing data for Rate {rate} and duration {duration}  is {ex}");
                throw;
            }
        }
    }
}

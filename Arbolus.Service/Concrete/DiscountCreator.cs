using Arbolus.Service.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbolus.Service.Concrete
{
    public class DiscountCreator
    {
        public DiscountCreator(ILogger logger, IConfiguration configuration)
        {
            Discounts = new Dictionary<string, Discount>();
            Discounts.Add("FOLLOW", new FollowUp(logger, configuration));
            Discounts.Add("1HOURAGREEMENT", new HourAgreement(logger, configuration));
            Discounts.Add("DEFAULT", new HourAgreement(logger, configuration));
        }

        public Dictionary<string, Discount> Discounts { get; set; }
    }
}

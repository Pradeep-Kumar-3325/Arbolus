using Arbolus.Data;
using Arbolus.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbolus.Service
{
    public static class Currency
    {

        public static decimal Convert(CurrencyEnum source, CurrencyEnum destination, decimal hourRate)
        {
           var conversionRate = ConversionRate(source, destination);
           return hourRate * conversionRate;
        }

        public static decimal ConversionRate(CurrencyEnum source, CurrencyEnum destination)
        {
            // Can use Reflection also for full dynamic 
            switch (source)
            {
                case CurrencyEnum.USD:
                    if (RateData.rates.USD.ContainsKey(destination.ToString()))
                        return RateData.rates.USD[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.AUD:
                    if (RateData.rates.AUD.ContainsKey(destination.ToString()))
                        return RateData.rates.AUD[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.CAD:
                    if (RateData.rates.CAD.ContainsKey(destination.ToString()))
                        return RateData.rates.CAD[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.EUR:
                    if (RateData.rates.EUR.ContainsKey(destination.ToString()))
                        return RateData.rates.EUR[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.GBP:
                    if (RateData.rates.GBP.ContainsKey(destination.ToString()))
                        return RateData.rates.GBP[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.JPY:
                    if (RateData.rates.JPY.ContainsKey(destination.ToString()))
                        return RateData.rates.JPY[destination.ToString()];
                    else
                        return 1;
                default:
                        return 1;
            }
        }
    }
}

using Arbolus.Data;
using Arbolus.Model.Concrete;
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

        public static decimal Convert(CurrencyEnum source, CurrencyEnum destination, decimal hourRate, Rate rates)
        {
           var conversionRate = ConversionRate(source, destination, rates);
           return hourRate * conversionRate;
        }

        public static decimal ConversionRate(CurrencyEnum source, CurrencyEnum destination, Rate rates)
        {
            // Can use Reflection also for full dynamic 
            switch (source)
            {
                case CurrencyEnum.USD:
                    if (rates.USD.ContainsKey(destination.ToString()))
                        return rates.USD[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.AUD:
                    if (rates.AUD.ContainsKey(destination.ToString()))
                        return rates.AUD[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.CAD:
                    if (rates.CAD.ContainsKey(destination.ToString()))
                        return rates.CAD[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.EUR:
                    if (rates.EUR.ContainsKey(destination.ToString()))
                        return rates.EUR[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.GBP:
                    if (rates.GBP.ContainsKey(destination.ToString()))
                        return rates.GBP[destination.ToString()];
                    else
                        return 1;
                case CurrencyEnum.JPY:
                    if (rates.JPY.ContainsKey(destination.ToString()))
                        return rates.JPY[destination.ToString()];
                    else
                        return 1;
                default:
                        return 1;
            }
        }
    }
}

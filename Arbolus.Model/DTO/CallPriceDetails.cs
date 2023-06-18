using System;

namespace Arbolus.Model
{
    public class CallPriceDetails
    {
        public string Expert { get; set; }

        public string Client { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public decimal Currency { get; set; }
    }
}

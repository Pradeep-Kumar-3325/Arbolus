using System.Collections.Generic;

namespace Arbolus.Model.Concrete
{
    public class Rate
    {
        public Dictionary<string,decimal> AUD { get; set; }

        public Dictionary<string, decimal> CAD { get; set; }

        public Dictionary<string, decimal> EUR { get; set; }

        public Dictionary<string, decimal> GBP { get; set; }

        public Dictionary<string, decimal> JPY { get; set; }

        public Dictionary<string, decimal> USD { get; set; }
    }
}

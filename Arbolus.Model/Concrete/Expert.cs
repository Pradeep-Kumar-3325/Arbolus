using System.Collections.Generic;

namespace Arbolus.Model.Concrete
{
    public class Expert
    {
        public string currency { get; set; }

        public decimal hourlyRate { get; set; }

        public string Name { get; set; }

        public List<Call> Calls { get; set; }
    }
}

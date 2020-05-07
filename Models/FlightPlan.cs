using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi.Models
{
    public class FlightPlan
    {
        public int passengers { get; set; }
        public string company_name { get; set; }
        public InitialLocation initial_location { get; set; }
        public List<Segments> segments = new List<Segments>();
        public int Id { get; set; }
    }
}

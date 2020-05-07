using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi.Models
{
    public class FlightPlan
    {
        public string company_name { get; set; }

        public int passengers { get; set; }

        public InitialLocation initial_location { get; set; }

        public List<Segments> segments = new List<Segments>();


    }
}

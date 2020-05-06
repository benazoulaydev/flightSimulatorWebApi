using flightSimulatorWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi.Services
{
    public interface IFlightPlanServices
    {
        FlightPlan AddFlightPlan(FlightPlan infos);

        FlightPlan GetFlightPlanById(int id);
        Dictionary<int, FlightPlan> GetFlightPlans();
    }
}

using flightSimulatorWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi.Services
{

    public class FlightPlanServices : IFlightPlanServices
    {
        private readonly Dictionary<int, FlightPlan> _flightPlan;
        private int idC = 1;
        public FlightPlanServices()
        {
            _flightPlan = new Dictionary<int, FlightPlan>();
        }
        public FlightPlan AddFlightPlan(FlightPlan infos)
        {
            return infos;
            /*            infos.Id = idC;
                        _flightPlan.Add(infos.Id, infos);
                        idC++;
                        return infos;*/
        }


        public FlightPlan GetFlightPlanById(int id)
        {
            FlightPlan outFlightPlan;
            if (_flightPlan.TryGetValue(id, out outFlightPlan))
            {
                return outFlightPlan;
            }
            else
            {
                return null;
            }
        }

        public Dictionary<int, FlightPlan> GetFlightPlans()
        {
            return _flightPlan;
        }
    }
}

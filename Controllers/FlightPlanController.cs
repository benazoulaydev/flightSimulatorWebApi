using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightSimulatorWebApi.Models;
using flightSimulatorWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace flightSimulatorWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private IMemoryCache _cache;

        public FlightPlanController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpPost]
        [Route("FlightPlan")]
        public ActionResult<FlightPlan> AddFlightPlan(FlightPlan infos)
        {
            int flightPlanID;
            Dictionary<int, FlightPlan> flightPlans;
            if (!_cache.TryGetValue("FlightPlanID", out flightPlanID))
            {
                _cache.Set("FlightPlanID", 0);
            }
            //check if no value exist.
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                flightPlans = new Dictionary<int, FlightPlan>();
                _cache.Set("FlightPlans", flightPlans);
            }
            //add anyway to cache
            flightPlans.Add(flightPlanID, infos);
            _cache.Set("FlightPlanID", flightPlanID + 1);
            return Ok(infos);

            /* var flightPlan = _services.AddFlightPlan(infos);

             if (flightPlan == null)
             {
                 return NotFound();
             }
             return Ok(flightPlan);*/
        }
        [HttpGet]
        [Route("FlightPlan/{id:int}")]
        public ActionResult<FlightPlan> GetFlightPlanById(int id)
        {
            Dictionary<int, FlightPlan> flightPlans;
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                return NotFound();
            }
            FlightPlan flightPlan;
            if (!flightPlans.TryGetValue(id, out flightPlan))
            {
                return NotFound();
            }
            return flightPlan;
            
            /* var flightPlan = _services.GetFlightPlanById(id);
             if (flightPlan == null)
             {
                 return NotFound();
             }
             return flightPlan;*/
        }


        /*
         * return all flight plans 
         * 
        [Route("FlightPlan")]
        public ActionResult<Dictionary<int, FlightPlan>> GetFlightPlans()
        {
            var flightPlan = _services.GetFlightPlans();
            if (flightPlan.Count == 0)
            {
                return NotFound();
            }
            return flightPlan;
        }*/

    }
}
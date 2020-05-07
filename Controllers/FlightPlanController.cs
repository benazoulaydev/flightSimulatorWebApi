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
        private readonly IFlightPlanServices _services;
        private IMemoryCache _cache;
        private int idC = 1;

        public FlightPlanController(IFlightPlanServices services, IMemoryCache cache)
        {
            _services = services;
            _cache = cache;
        }

        [HttpPost]
        [Route("FlightPlan")]
        public ActionResult<FlightPlan> AddFlightPlan(FlightPlan infos)
        {
            var flightPlan = _cache.Set(idC, infos);
            idC++;
            return Ok(flightPlan);

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
            FlightPlan outFlightPlan;
            if (_cache.TryGetValue(id, out outFlightPlan))
            {
                return outFlightPlan;
            }
            else
            {
                return NotFound();
            }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightSimulatorWebApi.Models;
using flightSimulatorWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace flightSimulatorWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private readonly IFlightPlanServices _services;

        public FlightPlanController(IFlightPlanServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("FlightPlan")]
        public ActionResult<FlightPlan> AddFlightPlan(FlightPlan infos)
        {
            var flightPlan = _services.AddFlightPlan(infos);
            Console.WriteLine("hicc");
            if (flightPlan == null)
            {
                return NotFound();
            }
            return Ok(flightPlan);
        }
        [HttpGet]
        [Route("FlightPlan/{id:int}")]
        public ActionResult<FlightPlan> GetFlightPlanById(int id)
        {
            Console.WriteLine("hicc");
            var flightPlan = _services.GetFlightPlanById(id);
            if (flightPlan == null)
            {
                return NotFound();
            }
            return flightPlan;
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
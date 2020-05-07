using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using flightSimulatorWebApi.Models;

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

        /*        [Route("Flights?relative_to={date_time:DateTime}")]*/
        [HttpGet]
        [Route("Flights")]
        public ActionResult<List<Flight>> GetFlightsByDate(DateTime relative_to)
        {
            //TODO 
            List<Flight> flightList = new List<Flight>();

            Dictionary<int, FlightPlan> flightPlans;
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                return NotFound();
            }

            foreach (KeyValuePair<int, FlightPlan> entry in flightPlans)
            {
                DateTime entryKeyTimeAfter = entry.Value.initial_location.Time;
                DateTime entryKeyTimeBefore = entry.Value.initial_location.Time;
                if (entry.Value.initial_location.Time > relative_to)
                {
                    continue;
                }

                double latitudeBefore = entry.Value.initial_location.Latitude;
                double longitudeBefore = entry.Value.initial_location.Longitude;

                foreach (var segment in entry.Value.segments)
                {

                    double latitudeAfter = segment.Latitude;
                    double longitudeAfter = segment.Longitude;

                    entryKeyTimeAfter = entryKeyTimeAfter.AddSeconds(segment.TimeSpan);

                    if (entryKeyTimeBefore <= relative_to && entryKeyTimeAfter >= relative_to)
                    {
                        Flight tmp = new Flight();
                        tmp.flight_id = entry.Key.ToString();
                        tmp.passengers = entry.Value.passengers;
                        tmp.company_name = entry.Value.company_name;
                        tmp.date_time = entry.Value.initial_location.Time;
                        //TODO lontitude lattitude to change proportinally

                        double prop = propFinder(entryKeyTimeBefore, entryKeyTimeAfter, relative_to);

                        tmp.longitude = LongituteFinder(longitudeBefore, longitudeAfter, prop);
                        tmp.latitude = LatitudeFinder(latitudeBefore, latitudeAfter, prop);
                        tmp.is_external = false;

                        flightList.Add(tmp);
                        break;
                    }
                    entryKeyTimeBefore.AddSeconds(segment.TimeSpan);

                    latitudeBefore = segment.Latitude;
                    longitudeBefore = segment.Longitude;

                }

                // do something with entry.Value or entry.Key
            }

            if (flightList.Count == 0)
            {
                return NotFound();
            }
            return flightList;


        }
        public double propFinder(DateTime TimeBefore, DateTime TimeAfter, DateTime Between)
        {
            double tmp = (Between - TimeBefore) / (TimeAfter - TimeBefore);
            return tmp;
        }
        public double LongituteFinder(double firstY, double secondY, double prop)
        {
            double Dy = secondY - firstY;
            return firstY + (Dy * prop);
        }
        public double LatitudeFinder(double firstX, double secondX, double prop)
        {
            double Dx = secondX - firstX;
            return firstX + (Dx * prop);
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
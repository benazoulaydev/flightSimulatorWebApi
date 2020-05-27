﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using flightSimulatorWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace flightSimulatorWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        Mutex mut = new Mutex();
        Random rand = new Random();
        private IMemoryCache _cache;
        private HttpClient _client;

        // get cache shared memory
        public FlightPlanController(IMemoryCache cache, IHttpClientFactory factory)
        {
            _cache = cache;
            _client = factory.CreateClient("api");
        }

        [HttpPost]
        [Route("FlightPlan")]
        public ActionResult<FlightPlan> AddFlightPlan(FlightPlan infos)
        {
            string flightPlanID;
            Dictionary<string, FlightPlan> flightPlans;
            //check if no value exist.
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                flightPlans = new Dictionary<string, FlightPlan>();
                _cache.Set("FlightPlans", flightPlans);
            }
            //add anyway to cache and pik uniq id for it
            do
            {
                flightPlanID = GetFlightID();
            } while (flightPlans.ContainsKey(flightPlanID));
            flightPlans.Add(flightPlanID, infos);
            return Ok(infos);
        }
        [HttpGet]
        [Route("FlightPlan/{id}")]
        public async Task<ActionResult<FlightPlan>> GetFlightPlanById(string id)
        {
            Dictionary<string, FlightPlan> flightPlans;
            // check if there any flightPlan has added ever
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                Task<ActionResult<FlightPlan>> tmp = ServerIdFlightPlan(id);
                try
                {
                    return await tmp;
                }
                catch (Exception e)
                {
                    return NotFound();
                }

            }
            FlightPlan flightPlan;
            // extract the FlightPlan object which asked for
            if (!flightPlans.TryGetValue(id, out flightPlan))
            {
                Task<ActionResult<FlightPlan>> tmp = ServerIdFlightPlan(id);
                try
                {
                    return await tmp;
                }
                catch (Exception e)
                {
                    return NotFound();
                }

            }
            return Ok(flightPlan); ;
        }
        public async Task<ActionResult<FlightPlan>> ServerIdFlightPlan(string id)
        {
            Dictionary<string, Servers> servers;
            if (_cache.TryGetValue("servers", out servers))
            {
                foreach (KeyValuePair<string, Servers> server in servers)
                {
                    HttpResponseMessage response = await _client.GetAsync(server.Value.ServerURL + "/api/FlightPlan/" + id);
                    response.EnsureSuccessStatusCode();
                    var resp = await response.Content.ReadAsStringAsync();

                    FlightPlan serverflightPlan = JsonConvert.DeserializeObject<FlightPlan>(resp);

                    return Ok(serverflightPlan);



                }
            }
            else
            {
                return NotFound();
            }
            return NotFound();
        }
        /*[Route("Flights?relative_to={date_time:DateTime}")]*/
        [HttpGet]
        [Route("Flights")]
        public async Task<ActionResult<List<Flight>>> GetFlightsByDateAsync(DateTime relative_to)
        {
            List<Flight> flightList = new List<Flight>();
            Dictionary<string, FlightPlan> flightPlans;
            // check if there any flight at all
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                return Ok(flightList); // return empty list
            }

            // going throw each flight
            foreach (KeyValuePair<string, FlightPlan> entry in flightPlans)
            {
                DateTime entryKeyTimeAfter = entry.Value.initial_location.date_time;
                DateTime entryKeyTimeBefore = entry.Value.initial_location.date_time;
                if (entry.Value.initial_location.date_time > relative_to)
                {
                    continue;
                }

                double latitudeBefore = entry.Value.initial_location.latitude;
                double longitudeBefore = entry.Value.initial_location.longitude;

                foreach (var segment in entry.Value.segments)
                {

                    double latitudeAfter = segment.latitude;
                    double longitudeAfter = segment.longitude;

                    entryKeyTimeAfter = entryKeyTimeAfter.AddSeconds(segment.timespan_seconds);

                    if (entryKeyTimeBefore <= relative_to && entryKeyTimeAfter >= relative_to)
                    {
                        Flight tmp = new Flight();
                        tmp.flight_id = entry.Key.ToString();
                        tmp.passengers = entry.Value.passengers;
                        tmp.company_name = entry.Value.company_name;
                        tmp.date_time = entry.Value.initial_location.date_time;

                        double prop = propFinder(entryKeyTimeBefore, entryKeyTimeAfter, relative_to);

                        tmp.longitude = LongituteFinder(longitudeBefore, longitudeAfter, prop);
                        tmp.latitude = LatitudeFinder(latitudeBefore, latitudeAfter, prop);
                        tmp.is_external = false;

                        flightList.Add(tmp);
                        break;
                    }
                    entryKeyTimeBefore.AddSeconds(segment.timespan_seconds);

                    latitudeBefore = segment.latitude;
                    longitudeBefore = segment.longitude;
                }
            }
            //check from other servers
            if (Request.QueryString.Value.Contains("sync_all"))
            {
                Dictionary<string, Servers> servers;
                if (_cache.TryGetValue("servers", out servers))
                {
                    foreach (KeyValuePair<string, Servers> server in servers)
                    {
                        try
                        {
                            HttpResponseMessage response = await _client.GetAsync(server.Value.ServerURL + "/api/Flights?relative_to=" + relative_to.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                            response.EnsureSuccessStatusCode();
                            var resp = await response.Content.ReadAsStringAsync();

                            List<Flight> serverFlights = JsonConvert.DeserializeObject<List<Flight>>(resp);

                            // change to external
                            foreach (Flight flight in serverFlights)
                            {
                                flight.is_external = true;
                            }

                            //merg lists
                            flightList = flightList.Concat(serverFlights).ToList();
                        }
                        catch (NullReferenceException e)
                        {
                            break;
                        }


                    }
                }
            }
            return Ok(flightList);
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        public ActionResult<FlightPlan> DeleteFlight(string id)
        {
            Dictionary<string, FlightPlan> flightPlans;
            if (!_cache.TryGetValue("FlightPlans", out flightPlans))
            {
                return NotFound();
            }
            FlightPlan flightPlan;
            if (!flightPlans.TryGetValue(id, out flightPlan))
            {
                return NotFound();
            }
            flightPlans.Remove(id);
            return Ok(id);
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

        private string GetFlightID()
        {
            string bigLs;
            string smallLs;
            string digs;
            string newCode;

            mut.WaitOne();

            bigLs = GetBigLetters(rand.Next(2, 3));
            smallLs = GetSmallLetters(rand.Next(2, 3));
            digs = GetDigits(rand.Next(2, 3));

            newCode = (bigLs + smallLs + digs);

            mut.ReleaseMutex();
            return newCode;
        }

        private string GetBigLetters(int n)
        {
            int codeN;
            char codeC;
            string result = "";
            for (int i = 0; i < n; i++)
            {
                codeN = rand.Next(65, 91);
                codeC = (char)codeN;
                result += codeC;
            }
            return result;
        }

        private string GetSmallLetters(int n)
        {
            int codeN;
            char codeC;
            string result = "";
            for (int i = 0; i < n; i++)
            {
                codeN = rand.Next(97, 123);
                codeC = (char)codeN;
                result += codeC;
            }
            return result;
        }

        private string GetDigits(int n)
        {
            int codeN;
            string result = "";
            for (int i = 0; i < n; i++)
            {
                codeN = rand.Next(0, 10);
                result += codeN.ToString();
            }
            return result;
        }
    }
}
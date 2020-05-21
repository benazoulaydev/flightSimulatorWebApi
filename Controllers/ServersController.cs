using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using flightSimulatorWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace flightSimulatorWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private IMemoryCache _cache;
        private HttpClient _client;

        public ServersController(IMemoryCache cache, IHttpClientFactory factory)
        {
            _cache = cache;
            _client = factory.CreateClient("api");
        }

        [HttpPost]
        [Route("servers")]
        public ActionResult<Servers> AddServer(Servers infos)
        {
            Console.WriteLine(infos);
            //string serversID;
            Dictionary<string, Servers> servers;

            //check if no value exist.
            if (!_cache.TryGetValue("servers", out servers))
            {
                servers = new Dictionary<string, Servers>();
                _cache.Set("servers", servers);
            }
            //add anyway to cache
            servers.Add(infos.ServerId, infos);
            return Ok(infos);
        }

        [HttpGet]
        [Route("servers")]
        public ActionResult<List<Servers>> GetServers()
        {
            Dictionary<int, Servers> servers;
            if (!_cache.TryGetValue("servers", out servers))
            {
                return NotFound();
            }
            return servers.Values.ToList();
        }


        [HttpDelete]
        [Route("servers/{id:int}")]
        public ActionResult<Servers> DeleteServerById(int id)
        {
            Dictionary<int, Servers> servers;
            if (!_cache.TryGetValue("servers", out servers))
            {
                return NotFound();
            }
            Servers server;
            if (!servers.TryGetValue(id, out server))
            {
                return NotFound();
            }
            servers.Remove(id);
            return server;
        }

    }
}
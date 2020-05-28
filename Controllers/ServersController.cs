using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlightSimulatorWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

/// <summary>
/// 
/// </summary>
namespace FlightSimulatorWebApi.Controllers
{
    /// <summary>
    /// api servers controllers
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        /// <summary>
        /// The cache memory of the api
        /// </summary>
        private IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServersController"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public ServersController(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Adds the server.
        /// </summary>
        /// <param name="infos">The infos.</param>
        /// <returns></returns>
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
            return infos;
        }

        /// <summary>
        /// Gets the servers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("servers")]
        public ActionResult<List<Servers>> GetServers()
        {
            Dictionary<string, Servers> servers;
            if (!_cache.TryGetValue("servers", out servers))
            {
                return NotFound();
            }
            return servers.Values.ToList();
        }

        /// <summary>
        /// Deletes the server by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("servers/{id}")]
        public ActionResult<Servers> DeleteServerById(string id)
        {
            Dictionary<string, Servers> servers;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Servers model
/// </summary>
namespace FlightSimulatorWebApi.Models
{
    /// <summary>
    /// Servers object
    /// </summary>
    public class Servers
    {
        /// <summary>
        /// Gets or sets the server identifier.
        /// </summary>
        /// <value>
        /// The server identifier.
        /// </value>
        public string ServerId { get; set; }

        /// <summary>
        /// Gets or sets the server URL.
        /// </summary>
        /// <value>
        /// The server URL.
        /// </value>
        public string ServerURL { get; set; }
    }
}

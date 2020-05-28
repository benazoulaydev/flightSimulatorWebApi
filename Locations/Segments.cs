using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Segments model
/// </summary>
namespace FlightSimulatorWebApi
{
    /// <summary>
    /// Segments object
    /// </summary>
    public class Segments
    {
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double latitude { get; set; }

        /// <summary>
        /// Gets or sets the timespan seconds.
        /// </summary>
        /// <value>
        /// The timespan seconds.
        /// </value>
        public double timespan_seconds { get; set; }
    }
}

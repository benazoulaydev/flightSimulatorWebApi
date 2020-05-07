using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi
{
    public class Segments
    {
        private readonly int longitude; // field
        public int Longitude   // property
        {
            get { return longitude; }   // get method
        }

        private readonly int latitude; // field
        public int Latitude   // property
        {
            get { return latitude; }   // get method
        }

        private readonly int timeSpan; // field
        public int TimeSpan   // property
        {
            get { return timeSpan; }   // get method
        }
        public Segments(int longitude, int latitude, int timespan_seconds)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.timeSpan = timespan_seconds;
        }
    }
}

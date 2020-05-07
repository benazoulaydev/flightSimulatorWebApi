using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi
{
    public class Segments
    {
        private readonly double longitude; // field
        public double Longitude   // property
        {
            get { return longitude; }   // get method
        }

        private readonly double latitude; // field
        public double Latitude   // property
        {
            get { return latitude; }   // get method
        }

        private readonly double timeSpan; // field
        public double TimeSpan   // property
        {
            get { return timeSpan; }   // get method
        }
        public Segments(double longitude, double latitude, double timespan_seconds)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.timeSpan = timespan_seconds;
        }
    }
}

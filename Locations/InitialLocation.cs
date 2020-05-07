using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi
{
    public class InitialLocation
    {
        private double longitude; // field
        public double Longitude   // property
        {
            get { return longitude; }   // get method
        }

        private double latitude; // field
        public double Latitude   // property
        {
            get { return latitude; }   // get method
        }

        private DateTime time; // field
        public DateTime Time   // property
        {
            get { return time; }   // get method
        }
        public InitialLocation(double longitude, double latitude, DateTime date_time)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.time = date_time;
        }
    }
}

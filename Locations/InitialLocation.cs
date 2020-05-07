using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightSimulatorWebApi
{
    public class InitialLocation
    {
        private int longitude; // field
        public int Longitude   // property
        {
            get { return longitude; }   // get method
        }

        private int latitude; // field
        public int Latitude   // property
        {
            get { return latitude; }   // get method
        }

        private string time; // field
        public string Time   // property
        {
            get { return time; }   // get method
        }
        public InitialLocation(int longitude, int latitude, string date_time)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.time = date_time;
        }
    }
}

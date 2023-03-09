using System;
using UnityEngine;

namespace Generic
{
    [Serializable]
    public class Coordinates
    {
        public double latitude = -1;
        public double longitude = -1;
        public double altitude;
        public int importance;

        public Coordinates() { }

        public Coordinates(float latitude, float longitude, float altitude, int importance = 0)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            this.altitude = altitude;
            this.importance = importance;
        }

        public bool IsValid()
        {
            if (this.latitude == -1 || this.longitude == -1)
            {
                return false;
            }
            return true;
        }
        
        public bool IsImportant()
        {
            return importance >= 0;
        }
        public double DistanceTo(Coordinates coordinates)
        {
            return DistanceTo(coordinates.latitude, coordinates.longitude);
        }

        public double DistanceTo(double toLat, double toLon)
        {
            double rlat1 = Math.PI*latitude/180;
            double rlat2 = Math.PI*toLat/180;
            double theta = longitude - toLon;
            double rtheta = Math.PI*theta/180;
            double dist =
                Math.Sin(rlat1)*Math.Sin(rlat2) + Math.Cos(rlat1)*
                Math.Cos(rlat2)*Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist*180/Math.PI;
            dist = dist*60*1.1515;

            dist *= 1.609344;

            return dist;
        }
    }
}
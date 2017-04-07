using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTService.Controllers.Utility
{
    public static class DistanceCalculator
    {
        public static double distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;

            return (dist);
        }

        // Converts degrees to radians
        // Used in distance calculation
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        // Converts radians to degrees
        // Used in distance calculation
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
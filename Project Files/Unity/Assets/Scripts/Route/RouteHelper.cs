using System;
using System.Collections.Generic;
using Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Route
{
    public class RouteHelper
    {
        /// <summary>
        /// Create a route from a json string.
        /// </summary>
        public static Route CreateRouteFromJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Route>(json);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Creates json from route object using json.net
        /// </summary>
        public static string CreateJsonFromRoute(Route route)
        {
            try
            {
                return JsonConvert.SerializeObject(route);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Debug route for testing purposes.
        /// </summary>
        /// <returns></returns>
        //public static Route GetDebugRoute()
        //{
        //    return new Route()
        //    {
        //        routeName = "debugRouteName",
        //        PointsOfInterest = new List<RoutePoint>()
        //        {
        //            new RoutePoint()
        //            {
        //              pointName  = "A",
        //              Coordinates = new Coordinates()
        //              {
        //                  latitude = 52.390652,
        //                  longitude = 4.856711
        //              }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "B",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.390283,
        //                    longitude = 4.855105
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "C",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.390256,
        //                    longitude = 4.853196
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "D",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.389640,
        //                    longitude = 4.851188
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "E",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.389582,
        //                    longitude = 4.849565
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "F",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.389521,
        //                    longitude = 4.846371
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "G",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.389477,
        //                    longitude = 4.841773
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "H",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.389513,
        //                    longitude = 4.839421
        //                }
        //            },
        //            new RoutePoint()
        //            {
        //                pointName  = "I",
        //                Coordinates = new Coordinates()
        //                {
        //                    latitude = 52.389162,
        //                    longitude = 4.838407
        //                }
        //            },
        //        }
        //    };
        //}
    }
}
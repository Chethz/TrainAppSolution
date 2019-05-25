using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cheth
{
    public class Logic
    {
        //to store vertise and edges to required vertices
        private RailNetwork _RailNetwork = new RailNetwork();

        //Split user inputs by , and break them in to sores city, destination city and distance as per user input
        //pass values to AddEdge function
        public void ProcessRoutes(string routes)
        {
            try
            {
                string[] routeArray = routes.Split(',');

                foreach (var route in routeArray)
                {
                    _RailNetwork.CreateEdge(route);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        //Calculate distance of given routes
        public int DistanceOfRoutes(string journey)
        {
            try
            {
                int distance = 0;
                Town sourseTown = null;
                char destinationTown = '\0';
                char[] travelRoute = journey.ToCharArray();


                for (int i = 0; i < travelRoute.Length-1; i++)
                {
                    //check for char is a character
                    if(!Char.IsLetter(travelRoute[i]))
                        throw new Exception(ErrorMessages.InvalidInput);
                    
                    sourseTown = _RailNetwork.GetTown(travelRoute[i]);
                    destinationTown = travelRoute[i + 1];

                    if (sourseTown != null && sourseTown.IsRouteExists(destinationTown))
                    {
                        distance += sourseTown.Distance(destinationTown);
                    }
                    else
                    {
                        throw new Exception(ErrorMessages.NoRouteFound);
                    }
                }

                return distance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //check for empty town and throw exception
        //check for existing route for destination town
        //use foreach loop to input next destination city in to the recursive function
        public int NumberOfTripsWithMaximumStops(char sourceTown, char destinationTown, int maxStops, int totalStops,
            int totalTrips)
        {
            if (totalStops <= maxStops)
            {
                Town tempTown = _RailNetwork.GetTown(sourceTown);
                
                if (tempTown.IsRouteExists(destinationTown))
                {
                    return totalTrips + 1;
                }
                else
                {
                    foreach (var destinations in tempTown.DestinationList)
                    {
                        totalTrips = NumberOfTripsWithMaximumStops(destinations.DestinationTown.Name, destinationTown,
                            maxStops, totalStops + 1, totalTrips);
                    }
                }
            }

            return totalTrips;
        }

        //get number of trips with exat stops
        public int NumberOfTripsWithExactStops(char sourceTown, char destinationTown, int stops, int totalStops,
            int totalTrips)
        {
            if (totalStops < stops)
            {
                Town tempTown = _RailNetwork.GetTown(sourceTown);

                if (tempTown.IsRouteExists(destinationTown))
                {

                    return totalStops == stops ? totalTrips + 1 : totalTrips;
                }
                else
                {
                    foreach (var destinations in tempTown.DestinationList)
                    {
                        totalTrips = NumberOfTripsWithMaximumStops(destinations.DestinationTown.Name, destinationTown,
                            stops, totalStops + 1, totalTrips);
                    }
                }
            }

            return totalTrips;
        }

        //call NumberOfTripsWithMaximumStops funtion to check available routes from C to C
        public int StartingAndEndCwithThreeStops()
        {
            try
            {
               int trips = NumberOfTripsWithMaximumStops('C', 'C', 3, 0, 0);
               return trips;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //call NumberOfTripsWithMaximumStops funtion to check available routes from A to C
        public int StartingAtAAndEndCwithFourStops()
        {
            try
            {
                int trips = NumberOfTripsWithExactStops('A', 'C', 4, 0, 0);
                return trips;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Calculte shortes route
        //Check source town for null values
        //check for existing routes
        //update distance
        //if shortest distance grate it replace 
        //mark true for visited towns

        public int ShortestRoute(char sourceTown, char destiantionT, int distance, int shortestDistance, List<char> visited)
        {   
            Town tempTown = _RailNetwork.GetTown(sourceTown);
            if (tempTown.IsRouteExists(destiantionT))
            {
                distance += tempTown.Distance(destiantionT);
                shortestDistance = shortestDistance > distance || shortestDistance == 0 ? distance : shortestDistance;
                return shortestDistance;
            }
            else
            {
                foreach (Route route in tempTown.DestinationList.Where(town =>
                    !visited.Contains(town.DestinationTown.Name))) 
                {
                    visited.Add(sourceTown);
                    shortestDistance = ShortestRoute(route.DestinationTown.Name, destiantionT,
                        distance + route.Distance, shortestDistance, visited);
                }
            }

            return shortestDistance;
        }


        //Get different routes from given source to destination
        public int GetDifferentRoutes(char sorceTown, char destinationTown, int distance, int maxDistance,
            int routeCount)
        {
            var town = _RailNetwork.GetTown(sorceTown);

            distance = distance + town.Distance(destinationTown);
            var tempDistance = town.Distance(destinationTown);
            if (distance + tempDistance >= maxDistance) return routeCount;
            if (town.IsRouteExists(destinationTown))
            {
                routeCount = routeCount + 1;
            }

            foreach (Route route in town.DestinationList)
            {
                routeCount = GetDifferentRoutes(route.DestinationTown.Name, destinationTown,
                    distance + route.Distance,
                    maxDistance, routeCount);
            }

            return routeCount;
        }

        //calling ShortestRoute function
        public int ShortestRouteAtoC()
        {
            int shortestRoute = ShortestRoute('A', 'C', 0, 0, new List<char>());
            return shortestRoute;
        }

        //calling ShortestRoute function
        public int ShortestRouteBtoB()
        {
            int shortestRoute = ShortestRoute('B', 'B', 0, 0, new List<char>());
            return shortestRoute;
        }

        //calling GetDifferentRoutes 
        public int DifferentRoutesCtoCDistanceLessThanThirty()
        {
           int routes = GetDifferentRoutes('C', 'C', 0, 30, 0);
           return routes;
        }

    }
}

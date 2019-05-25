using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cheth
{
    public class RailNetwork
    {
        //adjacency list
        private List<Town> _RailMap = new List<Town>() ;
        char[] townNames;

        public List<Town> RailMapList
        {
            get { return _RailMap; }
        }

        //add cities to townNames array
        public void RailNetWork(int noOfTowns)
        {
            townNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }

        //Add routes to _RailMap
        //If input sourcename is not exists in the _RailMap town added in to the _RailMap
        //If input destinationName is not exists in the _RailMap town added in to the _RailMap
        //add new route for source town
        public void AddEdge(string sourceName, string destinationName, int distance)
        {
            var sourceTown = _RailMap.Where(town => town.Name.ToString() == sourceName).FirstOrDefault();

            if (sourceTown == null)
            {
                sourceTown = new Town(sourceName);
                _RailMap.Add(sourceTown);
            }

            var destinationTown = _RailMap.Where(town => town.Name.ToString() == destinationName).FirstOrDefault();

            if (destinationTown == null)
            {
                destinationTown = new Town(destinationName);
                _RailMap.Add(destinationTown);
            }

            sourceTown.AddRoute(destinationTown,distance);
        }

        //Check length of the input string if it < 3 throw error
        //Check first two inputs for letters
        //Check third input for number
        //pass parameters to the CreateEdge function
        public void CreateEdge(string route)
        {
            char[] splitRoute = route.ToCharArray();
            if (splitRoute.Length != 3)
                throw new Exception(ErrorMessages.InvalidRoute);
            if (!Char.IsLetter(splitRoute[0]) || !Char.IsLetter(splitRoute[1]))
                throw new Exception(ErrorMessages.InvalidSourceOrDestination);
            if (!Char.IsDigit(splitRoute[2]))
                throw new Exception(ErrorMessages.InvalidDistance);

            string sourceName = route[0].ToString();
            string destination = route[1].ToString();
            int.TryParse(route[2].ToString(), out int distance);

            this.AddEdge(sourceName, destination, distance);
        }

        //return town name from the _RailMap
        public Town GetTown(char name)
        {
            var town = _RailMap.Where(x => x.Name == name).FirstOrDefault();
            if (town == null)
            {
                throw new Exception(ErrorMessages.TownNotFound);
            }

            return town;
        }
    }
}

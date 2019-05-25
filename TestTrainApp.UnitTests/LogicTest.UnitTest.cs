using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cheth
{
    class LogicTest
    {
        public Logic logic = new Logic();

        [SetUp]
        public void Setup()
        {
            logic = new Logic();
            logic.ProcessRoutes("AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7");
        }


        [Test]
        public void DistanceOfRoutes_TestDistance_LengthOfArray()
        {
            var distance = logic.DistanceOfRoutes("ABC");

            Assert.AreEqual(9, distance);
        }

        [Test]
        public void DistanceOfRoutes_NoRoute_ExceptionNoRoute()
        {
            //var distance = logic.DistanceOfRoutes("ABF");

            var ex = Assert.Throws<Exception>(() => logic.DistanceOfRoutes("AED"));
            Assert.AreEqual(ErrorMessages.NoRouteFound, ex.Message);
        }

        [Test]
        public void NumberOfTripsWithMaximumStops_StartingAtCEndCWithMax3Stops_ShouldReturnTwo()
        {
            var routes = logic.NumberOfTripsWithMaximumStops('C', 'C', 3, 0, 0);
            Assert.AreEqual(2,routes);
        }

        [Test]
        public void NumberOfTripsWithExactStops_StartingAtAEndCWithExact4Stops_ShouldReturnThree()
        {
            var routes = logic.NumberOfTripsWithExactStops('A', 'C', 4, 0, 0);
            Assert.AreEqual(3, routes);
        }

        [Test]
        public void ShortestRoute_ShouldReturnShortestPath_ShouldReturnNine()
        {
            var routes = logic.ShortestRoute('A', 'C', 0, 0, new List<char>() );
            Assert.AreEqual(9, routes);
        }

        [Test]
        public void ShortestRoute_ShouldReturnShortestPathBtoB_ShouldReturnNine()
        {
            var routes = logic.ShortestRoute('B', 'B', 0, 0, new List<char>());
            Assert.AreEqual(9, routes);
        }

        [Test]
        public void GetDifferentRoutes_SoudReturnDifferentRoutes_ShouldReturn4()
        {
            var routes = logic.GetDifferentRoutes('C', 'C', 0, 30, 0);
            Assert.AreEqual(3, routes);
        }
        
    }
}

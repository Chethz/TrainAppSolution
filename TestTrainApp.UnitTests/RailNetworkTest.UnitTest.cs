using NUnit.Framework;
using System;

namespace Cheth
{
    public class Tests
    {

        public RailNetwork railNetwork = new RailNetwork();

        [SetUp]
        public void Setup()
        {
            railNetwork = new RailNetwork();
            //railNetwork.CreateEdge("AB1");
        }

        [Test]
        public void CreateEdge_LenthisMoreThanThree_ThrowException()
        {
            //Arrange
            string edges = "ABC5";

            //Assert
            var ex = Assert.Throws<Exception>(() => railNetwork.CreateEdge(edges));
            Assert.AreEqual(ErrorMessages.InvalidRoute, ex.Message);
        }

        [Test]
        public void CreateEdge_isNotALetter_ThrowException()
        {
            //Arrange
            string edges = "1B3";

            //Assert
            var ex = Assert.Throws<Exception>(() => railNetwork.CreateEdge(edges));
            Assert.AreEqual(ErrorMessages.InvalidSourceOrDestination, ex.Message);
        }

        [Test]
        public void CreateEdge_isThirdCharacterNotADigit_ThrowException()
        {
            //Arrange
            string edges = "ABC";

            //Assert
            var ex = Assert.Throws<Exception>(() => railNetwork.CreateEdge(edges));
            Assert.AreEqual(ErrorMessages.InvalidDistance, ex.Message);
        }

        [Test]
        public void CreateEdge_AddingNode_ShouldIncreseCount()
        {
            string edge = "AB5";

            railNetwork.CreateEdge(edge);

            Assert.AreEqual(2, railNetwork.RailMapList.Count);
        }


    }
}
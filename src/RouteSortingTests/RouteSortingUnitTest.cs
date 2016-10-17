using Microsoft.VisualStudio.TestTools.UnitTesting;
using RouteSorting;
using RouteSorting.Entities;
using System;
using System.Collections.Generic;

namespace RouteSortingTests
{
    [TestClass]
    public class RouteSortingUnitTest
    {
        private List<RouteCard> srcCardList_success;
        private List<RouteCard> srcCardList_failure;

        private IRouteSorter dictImpl;

        [TestInitialize]
        public void Startup()
        {
            srcCardList_success = new List<RouteCard>
            {
                new RouteCard { DepPlace = "Мельбурн", DestPlace = "Кельн" },
                new RouteCard { DepPlace = "Москва", DestPlace = "Париж" },
                new RouteCard { DepPlace = "Кельн", DestPlace = "Москва" }            
            };

            srcCardList_failure = new List<RouteCard>
            {
                new RouteCard { DepPlace = "Мельбурн", DestPlace = "Кельн" },
                new RouteCard { DepPlace = "Кельн", DestPlace = "Мельбурн" }
            };

            dictImpl = new RouteSorterDictImpl();
        }

        [TestMethod]
        public void RouteSort_Success()
        {
            var sortedList = dictImpl.MakeRoute(srcCardList_success);

            Assert.AreEqual(sortedList[0].DepPlace, "Мельбурн");
            Assert.AreEqual(sortedList[0].DestPlace, "Кельн");

            Assert.AreEqual(sortedList[1].DepPlace, "Кельн");
            Assert.AreEqual(sortedList[1].DestPlace, "Москва");

            Assert.AreEqual(sortedList[2].DepPlace, "Москва");
            Assert.AreEqual(sortedList[2].DestPlace, "Париж");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RouteSort_Failure()
        {
            var sortedList = dictImpl.MakeRoute(srcCardList_failure);
        }
    }
}
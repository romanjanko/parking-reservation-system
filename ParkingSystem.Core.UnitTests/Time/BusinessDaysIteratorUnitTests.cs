using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.UnitTests.Time
{
    [TestClass]
    public class BusinessDaysIteratorUnitTests
    {
        [TestMethod]
        public void GetNextBusinessDayDate_1()
        {
            var currentDate = new DateTime(2016, 6, 27); // Monday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 28), result);
        }

        [TestMethod]
        public void GetNextBusinessDayDate_2()
        {
            var currentDate = new DateTime(2016, 6, 28); // Tuesday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 29), result);
        }

        [TestMethod]
        public void GetNextBusinessDayDate_3()
        {
            var currentDate = new DateTime(2016, 6, 29); // Wednesday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 30), result);
        }

        [TestMethod]
        public void GetNextBusinessDayDate_4()
        {
            var currentDate = new DateTime(2016, 6, 30); // Thursday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 7, 1), result);
        }

        [TestMethod]
        public void GetNextBusinessDayDate_5()
        {
            var currentDate = new DateTime(2016, 7, 1); // Friday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 7, 4), result);
        }

        [TestMethod]
        public void GetNextBusinessDayDate_6()
        {
            var currentDate = new DateTime(2016, 7, 2); // Saturday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 7, 4), result);
        }

        [TestMethod]
        public void GetNextBusinessDayDate_7()
        {
            var currentDate = new DateTime(2016, 7, 3); // Sunday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetNextBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 7, 4), result);
        }

        [TestMethod]
        public void GetPreviousBusinessDayDate_1()
        {
            var currentDate = new DateTime(2016, 7, 3); // Sunday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 7, 1), result);
        }

        [TestMethod]
        public void GetPreviousBusinessDayDate_2()
        {
            var currentDate = new DateTime(2016, 7, 2); // Saturday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 7, 1), result);
        }

        [TestMethod]
        public void GetPreviousBusinessDayDate_3()
        {
            var currentDate = new DateTime(2016, 7, 1); // Friday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 30), result);
        }
        [TestMethod]
        public void GetPreviousBusinessDayDate_4()
        {
            var currentDate = new DateTime(2016, 6, 30); // Thursday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 29), result);
        }

        [TestMethod]
        public void GetPreviousBusinessDayDate_5()
        {
            var currentDate = new DateTime(2016, 6, 29); // Wednesday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 28), result);
        }

        [TestMethod]
        public void GetPreviousBusinessDayDate_6()
        {
            var currentDate = new DateTime(2016, 6, 28); // Tuesday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 27), result);
        }

        [TestMethod]
        public void GetPreviousBusinessDayDate_7()
        {
            var currentDate = new DateTime(2016, 6, 27); // Monday
            var businessDaysIterator = new BusinessDaysIterator(new WeekendDayUtils());

            var result = businessDaysIterator.GetPreviousBusinessDayDate(currentDate);

            Assert.AreEqual(new DateTime(2016, 6, 24), result);
        }
    }
}

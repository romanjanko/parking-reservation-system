using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Utils;
using System;

namespace ParkingSystem.Core.UnitTests.Utils
{
    [TestClass]
    public class DayOfWeekUtilsUnitTests
    {
        [TestMethod]
        public void CanGetDaysOffset()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(0, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Monday));
            Assert.AreEqual(1, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Tuesday));
            Assert.AreEqual(2, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Wednesday));
            Assert.AreEqual(3, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Thursday));
            Assert.AreEqual(4, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Friday));
            Assert.AreEqual(5, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Saturday));
            Assert.AreEqual(6, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Sunday));

            Assert.AreEqual(-6, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Monday));
            Assert.AreEqual(-5, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Tuesday));
            Assert.AreEqual(-4, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Wednesday));
            Assert.AreEqual(-3, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Thursday));
            Assert.AreEqual(-2, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Friday));
            Assert.AreEqual(-1, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Saturday));
            Assert.AreEqual(0, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(1, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Monday));
            Assert.AreEqual(2, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Tuesday));
            Assert.AreEqual(3, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Wednesday));
            Assert.AreEqual(4, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Thursday));
            Assert.AreEqual(5, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Friday));
            Assert.AreEqual(6, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Saturday));
            Assert.AreEqual(7, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Sunday));
        }
    }
}

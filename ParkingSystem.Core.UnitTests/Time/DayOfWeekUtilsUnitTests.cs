using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.UnitTests.Time
{
    [TestClass]
    public class DayOfWeekUtilsUnitTests
    {
        [TestMethod]
        public void CanGetDaysOffset_1()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(0, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Monday));
        }

        [TestMethod]
        public void CanGetDaysOffset_2()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(1, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Tuesday));
        }

        [TestMethod]
        public void CanGetDaysOffset_3()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(2, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Wednesday));
        }

        [TestMethod]
        public void CanGetDaysOffset_4()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(3, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Thursday));
        }

        [TestMethod]
        public void CanGetDaysOffset_5()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(4, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Friday));
        }

        [TestMethod]
        public void CanGetDaysOffset_6()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(5, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Saturday));
        }

        [TestMethod]
        public void CanGetDaysOffset_7()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(6, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanGetDaysOffset_8()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(-6, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Monday));
        }

        [TestMethod]
        public void CanGetDaysOffset_9()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(-5, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Tuesday));
        }

        [TestMethod]
        public void CanGetDaysOffset_10()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(-4, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Wednesday));
        }

        [TestMethod]
        public void CanGetDaysOffset_11()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(-3, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Thursday));
        }

        [TestMethod]
        public void CanGetDaysOffset_12()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(-2, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Friday));
        }

        [TestMethod]
        public void CanGetDaysOffset_13()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(-1, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Saturday));
        }

        [TestMethod]
        public void CanGetDaysOffset_14()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(0, dayOfWeekUtils.GetDaysOffset(DayOfWeek.Sunday, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_1()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(1, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Monday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_2()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(2, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Tuesday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_3()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(3, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Wednesday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_4()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(4, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Thursday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_5()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(5, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Friday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_6()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(6, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Saturday));
        }

        [TestMethod]
        public void CanConvertDayOfWeekToInt_7()
        {
            var dayOfWeekUtils = new DayOfWeekUtils();

            Assert.AreEqual(7, dayOfWeekUtils.DayOfWeekToInt(DayOfWeek.Sunday));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Utils;
using System;

namespace ParkingSystem.Core.UnitTests.Utils
{
    [TestClass]
    public class WeekOfYearToDateConvertorUnitTests
    {
        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());

            Assert.AreEqual(new DateTime(2014, 12, 29),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2014, 12, 30),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2014, 12, 31),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2015, 1, 1),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(2015, 1, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2015, 1, 3),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2015, 1, 4),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 1, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2015, 1, 11),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 2, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2015, 12, 28),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2015, 53, DayOfWeek.Monday));
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());

            Assert.AreEqual(new DateTime(2016, 1, 4),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 1, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2016, 1, 5),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 1, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2016, 1, 7),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 1, DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(2016, 1, 8),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 1, DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2016, 1, 10),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 1, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2016, 1, 16),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 2, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2016, 3, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 9, DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2016, 12, 31),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 52, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 1, 1),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2016, 52, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());

            Assert.AreEqual(new DateTime(2017, 1, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 1, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2017, 1, 3),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 1, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2017, 1, 7),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 1, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 1, 8),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 1, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2017, 1, 14),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 2, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 1, 15),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 2, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2017, 12, 25),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 52, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2017, 12, 30),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 52, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 12, 31),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2017, 52, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());

            Assert.AreEqual(new DateTime(2018, 1, 1),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 1, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2018, 1, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 1, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2018, 1, 6),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 1, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2018, 1, 7),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 1, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2018, 1, 13),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 2, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2018, 1, 14),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 2, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2018, 12, 24),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 52, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2018, 12, 29),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 52, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2018, 12, 30),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(2018, 52, DayOfWeek.Sunday));
        }
    }
}

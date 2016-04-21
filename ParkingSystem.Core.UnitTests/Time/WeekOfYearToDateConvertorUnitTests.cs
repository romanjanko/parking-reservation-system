using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.UnitTests.Time
{
    [TestClass]
    public class WeekOfYearToDateConvertorUnitTests
    {
        private WeekOfYearToDateConvertor GetWeekOfYearToDateConvertorNewInstance()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            return new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
        }

        //TODO - separate the methods into smaller ones
        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            Assert.AreEqual(new DateTime(2014, 12, 29),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2014, 12, 30),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2014, 12, 31),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2015, 1, 1),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(2015, 1, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2015, 1, 3),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2015, 1, 4),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2015, 1, 11),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 2 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2015, 12, 28),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2015, Week = 53 }, DayOfWeek.Monday));
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            Assert.AreEqual(new DateTime(2016, 1, 4),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2016, 1, 5),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2016, 1, 7),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(2016, 1, 8),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(2016, 1, 10),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2016, 1, 16),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 2 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2016, 3, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 9 }, DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2016, 12, 31),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 52 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 1, 1),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2016, Week = 52 }, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            Assert.AreEqual(new DateTime(2017, 1, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2017, 1, 3),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2017, 1, 7),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 1, 8),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2017, 1, 14),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 2 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 1, 15),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 2 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2017, 12, 25),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 52 }, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2017, 12, 30),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 52 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2017, 12, 31),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2017, Week = 52 }, DayOfWeek.Sunday));
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            Assert.AreEqual(new DateTime(2018, 1, 1),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2018, 1, 2),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(2018, 1, 6),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2018, 1, 7),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2018, 1, 13),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 2 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2018, 1, 14),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 2 }, DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2018, 12, 24),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 52 }, DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(2018, 12, 29),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 52 }, DayOfWeek.Saturday));
            Assert.AreEqual(new DateTime(2018, 12, 30),
                            weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(new WeekOfYear { Year = 2018, Week = 52 }, DayOfWeek.Sunday));
        }
    }
}

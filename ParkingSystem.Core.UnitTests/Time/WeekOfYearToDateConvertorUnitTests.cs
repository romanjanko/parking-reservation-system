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

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_1()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2014, 12, 29), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_2()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Tuesday);

            Assert.AreEqual(new DateTime(2014, 12, 30), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_3()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Wednesday);

            Assert.AreEqual(new DateTime(2014, 12, 31), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_4()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Thursday);

            Assert.AreEqual(new DateTime(2015, 1, 1), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_5()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Friday);

            Assert.AreEqual(new DateTime(2015, 1, 2), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_6()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2015, 1, 3), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_7()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 1 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2015, 1, 4), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_8()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 2 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2015, 1, 11), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2015_9()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2015, Week = 53 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2015, 12, 28), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_1()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2016, 1, 4), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_2()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Tuesday);

            Assert.AreEqual(new DateTime(2016, 1, 5), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_3()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Thursday);

            Assert.AreEqual(new DateTime(2016, 1, 7), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_4()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Friday);

            Assert.AreEqual(new DateTime(2016, 1, 8), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_5()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 1 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2016, 1, 10), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_6()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 2 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2016, 1, 16), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_7()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 9 }, DayOfWeek.Wednesday);

            Assert.AreEqual(new DateTime(2016, 3, 2), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_8()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 52 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2016, 12, 31), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2016_9()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2016, Week = 52 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2017, 1, 1), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_1()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2017, 1, 2), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_2()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Tuesday);

            Assert.AreEqual(new DateTime(2017, 1, 3), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_3()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2017, 1, 7), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_4()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 1 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2017, 1, 8), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_5()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 2 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2017, 1, 14), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_6()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 2 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2017, 1, 15), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_7()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 52 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2017, 12, 25), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_8()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 52 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2017, 12, 30), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2017_9()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2017, Week = 52 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2017, 12, 31), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_1()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2018, 1, 1), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_2()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Tuesday);

            Assert.AreEqual(new DateTime(2018, 1, 2), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_3()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2018, 1, 6), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_4()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 1 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2018, 1, 7), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_5()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 2 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2018, 1, 13), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_6()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 2 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2018, 1, 14), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_7()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 52 }, DayOfWeek.Monday);

            Assert.AreEqual(new DateTime(2018, 12, 24), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_8()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 52 }, DayOfWeek.Saturday);

            Assert.AreEqual(new DateTime(2018, 12, 29), result);
        }

        [TestMethod]
        public void CanGetDateForDayInWeekOfYearFor2018_9()
        {
            var weekOfYearToDateConvertor = GetWeekOfYearToDateConvertorNewInstance();

            var result = weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                new WeekOfYear { Year = 2018, Week = 52 }, DayOfWeek.Sunday);

            Assert.AreEqual(new DateTime(2018, 12, 30), result);
        }
    }
}

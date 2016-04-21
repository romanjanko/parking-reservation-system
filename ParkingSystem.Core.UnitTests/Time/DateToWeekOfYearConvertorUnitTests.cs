using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Time.Convertors;

namespace ParkingSystem.Core.UnitTests.Time
{
    [TestClass]
    public class DateToWeekOfYearConvertorUnitTests
    {
        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2014And2015_1()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2014, 12, 28));

            Assert.AreEqual(2014, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2014And2015_2()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2014, 12, 29));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2014And2015_3()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 1, 1));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2014And2015_4()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 1, 4));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2014And2015_5()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 1, 5));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(2, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_1()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 12, 27));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_2()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 12, 28));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(53, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_3()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 1, 1));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(53, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_4()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 1, 3));

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(53, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_5()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 1, 4));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_6()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 2, 28));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(8, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_7()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 2, 29));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(9, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_8()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 3, 5));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(9, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016_9()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 3, 6));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(9, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2016And2017_1()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 12, 24));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(51, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2016And2017_2()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 12, 27));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2016And2017_3()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 1, 1));

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2016And2017_4()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 1, 2));

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2016And2017_5()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 1, 14));

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(2, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_1()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 12, 18));

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(51, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_2()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 12, 27));

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_3()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 12, 31));

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_4()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 1));

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_5()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 2));

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_6()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 7));

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_7()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 8));

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(2, result.Week);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018_8()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            var result = dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 14));

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(2, result.Week);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Utils;
using System;

namespace ParkingSystem.Core.UnitTests.Utils
{
    [TestClass]
    public class DateToWeekOfYearConvertorUnitTests
    {
        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2014And2015()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            int year1, week1;
            int year2, week2;
            int year3, week3;
            int year4, week4;
            int year5, week5;

            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2014, 12, 28), out year1, out week1);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2014, 12, 29), out year2, out week2);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 1, 1), out year3, out week3);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 1, 4), out year4, out week4);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 1, 5), out year5, out week5);

            Assert.AreEqual(2014, year1);
            Assert.AreEqual(52, week1);
            Assert.AreEqual(2015, year2);
            Assert.AreEqual(1, week2);
            Assert.AreEqual(2015, year3);
            Assert.AreEqual(1, week3);
            Assert.AreEqual(2015, year4);
            Assert.AreEqual(1, week4);
            Assert.AreEqual(2015, year5);
            Assert.AreEqual(2, week5);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2015And2016()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            int year1, week1;
            int year2, week2;
            int year3, week3;
            int year4, week4;
            int year5, week5;
            int year6, week6;
            int year7, week7;
            int year8, week8;
            int year9, week9;

            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 12, 27), out year1, out week1);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2015, 12, 28), out year2, out week2);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 1, 1), out year3, out week3);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 1, 3), out year4, out week4);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 1, 4), out year5, out week5);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 2, 28), out year6, out week6);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 2, 29), out year7, out week7);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 3, 5), out year8, out week8);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 3, 6), out year9, out week9);

            Assert.AreEqual(2015, year1);
            Assert.AreEqual(52, week1);
            Assert.AreEqual(2015, year2);
            Assert.AreEqual(53, week2);
            Assert.AreEqual(2015, year3);
            Assert.AreEqual(53, week3);
            Assert.AreEqual(2015, year4);
            Assert.AreEqual(53, week4);
            Assert.AreEqual(2016, year5);
            Assert.AreEqual(1, week5);
            Assert.AreEqual(2016, year6);
            Assert.AreEqual(8, week6);
            Assert.AreEqual(2016, year7);
            Assert.AreEqual(9, week7);
            Assert.AreEqual(2016, year8);
            Assert.AreEqual(9, week8);
            Assert.AreEqual(2016, year9);
            Assert.AreEqual(9, week9);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2016And2017()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            int year1, week1;
            int year2, week2;
            int year3, week3;
            int year4, week4;
            int year5, week5;

            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 12, 24), out year1, out week1);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2016, 12, 27), out year2, out week2);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 1, 1), out year3, out week3);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 1, 2), out year4, out week4);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 1, 14), out year5, out week5);

            Assert.AreEqual(2016, year1);
            Assert.AreEqual(51, week1);
            Assert.AreEqual(2016, year2);
            Assert.AreEqual(52, week2);
            Assert.AreEqual(2016, year3);
            Assert.AreEqual(52, week3);
            Assert.AreEqual(2017, year4);
            Assert.AreEqual(1, week4);
            Assert.AreEqual(2017, year5);
            Assert.AreEqual(2, week5);
        }

        [TestMethod]
        public void CanGetWeekOfYearForTurnOfYears2017And2018()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();

            int year1, week1;
            int year2, week2;
            int year3, week3;
            int year4, week4;
            int year5, week5;
            int year6, week6;
            int year7, week7;
            int year8, week8;

            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 12, 18), out year1, out week1);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 12, 27), out year2, out week2);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2017, 12, 31), out year3, out week3);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 1), out year4, out week4);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 2), out year5, out week5);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 7), out year6, out week6);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 8), out year7, out week7);
            dateToWeekOfYearConvertor.GetWeekOfYear(new DateTime(2018, 1, 14), out year8, out week8);

            Assert.AreEqual(2017, year1);
            Assert.AreEqual(51, week1);
            Assert.AreEqual(2017, year2);
            Assert.AreEqual(52, week2);
            Assert.AreEqual(2017, year3);
            Assert.AreEqual(52, week3);
            Assert.AreEqual(2018, year4);
            Assert.AreEqual(1, week4);
            Assert.AreEqual(2018, year5);
            Assert.AreEqual(1, week5);
            Assert.AreEqual(2018, year6);
            Assert.AreEqual(1, week6);
            Assert.AreEqual(2018, year7);
            Assert.AreEqual(2, week7);
            Assert.AreEqual(2018, year8);
            Assert.AreEqual(2, week8);
        }
    }
}

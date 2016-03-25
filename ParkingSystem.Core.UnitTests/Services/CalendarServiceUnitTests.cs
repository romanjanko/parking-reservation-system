using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Services;
using ParkingSystem.Core.Utils;
using System.Collections.Generic;
using ParkingSystem.Core.Models;

namespace ParkingSystem.Core.UnitTests.Services
{
    [TestClass]
    public class CalendarServiceUnitTests
    {
        [TestMethod]
        public void CanGetCurrentYear()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
            var calendarService = new CalendarService(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);

            Assert.AreEqual(DateTime.Today.Year, calendarService.GetCurrentYear());
        }

        [TestMethod]
        public void CanGetCurrentWeekInYear()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
            var calendarService = new CalendarService(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);
            int year, week;

            dateToWeekOfYearConvertor.GetWeekOfYear(DateTime.Now.Date, out year, out week);

            Assert.AreEqual(week, calendarService.GetCurrentWeekInYear());
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
            var calendarService = new CalendarService(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);

            int previousYear1, previousWeek1;
            int previousYear2, previousWeek2;
            int previousYear3, previousWeek3;
            int previousYear4, previousWeek4;
            int previousYear5, previousWeek5;
            int previousYear6, previousWeek6;
            int previousYear7, previousWeek7;
            int previousYear8, previousWeek8;

            calendarService.GetPreviousWeekInYear(2016, 1, out previousYear1, out previousWeek1);
            calendarService.GetPreviousWeekInYear(2016, 9, out previousYear2, out previousWeek2);
            calendarService.GetPreviousWeekInYear(2016, 52, out previousYear3, out previousWeek3);
            calendarService.GetPreviousWeekInYear(2017, 1, out previousYear4, out previousWeek4);
            calendarService.GetPreviousWeekInYear(2017, 2, out previousYear5, out previousWeek5);
            calendarService.GetPreviousWeekInYear(2017, 30, out previousYear6, out previousWeek6);
            calendarService.GetPreviousWeekInYear(2018, 1, out previousYear7, out previousWeek7);
            calendarService.GetPreviousWeekInYear(2018, 2, out previousYear8, out previousWeek8);

            Assert.AreEqual(2015, previousYear1);
            Assert.AreEqual(53, previousWeek1);
            Assert.AreEqual(2016, previousYear2);
            Assert.AreEqual(8, previousWeek2);
            Assert.AreEqual(2016, previousYear3);
            Assert.AreEqual(51, previousWeek3);
            Assert.AreEqual(2016, previousYear4);
            Assert.AreEqual(52, previousWeek4);
            Assert.AreEqual(2017, previousYear5);
            Assert.AreEqual(1, previousWeek5);
            Assert.AreEqual(2017, previousYear6);
            Assert.AreEqual(29, previousWeek6);
            Assert.AreEqual(2017, previousYear7);
            Assert.AreEqual(52, previousWeek7);
            Assert.AreEqual(2018, previousYear8);
            Assert.AreEqual(1, previousWeek8);
        }

        [TestMethod]
        public void CanGetNextWeekInYear()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
            var calendarService = new CalendarService(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);

            int nextYear1, nextWeek1;
            int nextYear2, nextWeek2;
            int nextYear3, nextWeek3;
            int nextYear4, nextWeek4;
            int nextYear5, nextWeek5;
            int nextYear6, nextWeek6;
            int nextYear7, nextWeek7;
            int nextYear8, nextWeek8;

            calendarService.GetNextWeekInYear(2015, 53, out nextYear1, out nextWeek1);
            calendarService.GetNextWeekInYear(2016, 8, out nextYear2, out nextWeek2);
            calendarService.GetNextWeekInYear(2016, 51, out nextYear3, out nextWeek3);
            calendarService.GetNextWeekInYear(2016, 52, out nextYear4, out nextWeek4);
            calendarService.GetNextWeekInYear(2017, 1, out nextYear5, out nextWeek5);
            calendarService.GetNextWeekInYear(2017, 29, out nextYear6, out nextWeek6);
            calendarService.GetNextWeekInYear(2017, 52, out nextYear7, out nextWeek7);
            calendarService.GetNextWeekInYear(2018, 1, out nextYear8, out nextWeek8);

            Assert.AreEqual(2016, nextYear1);
            Assert.AreEqual(1, nextWeek1);
            Assert.AreEqual(2016, nextYear2);
            Assert.AreEqual(9, nextWeek2);
            Assert.AreEqual(2016, nextYear3);
            Assert.AreEqual(52, nextWeek3);
            Assert.AreEqual(2017, nextYear4);
            Assert.AreEqual(1, nextWeek4);
            Assert.AreEqual(2017, nextYear5);
            Assert.AreEqual(2, nextWeek5);
            Assert.AreEqual(2017, nextYear6);
            Assert.AreEqual(30, nextWeek6);
            Assert.AreEqual(2018, nextYear7);
            Assert.AreEqual(1, nextWeek7);
            Assert.AreEqual(2018, nextYear8);
            Assert.AreEqual(2, nextWeek8);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
            var calendarService = new CalendarService(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);

            IList<DateTime> dates1, dates2, dates3, dates4, dates5, dates6;

            dates1 = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2015, Week = 1 });
            dates2 = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2016, Week = 1 });
            dates3 = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2017, Week = 1 });
            dates4 = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2018, Week = 1 });
            dates5 = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2016, Week = 10 });
            dates6 = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2016, Week = 52 });

            Assert.AreEqual(new DateTime(2014, 12, 29), dates1[0]);
            Assert.AreEqual(new DateTime(2014, 12, 30), dates1[1]);
            Assert.AreEqual(new DateTime(2014, 12, 31), dates1[2]);
            Assert.AreEqual(new DateTime(2015, 1, 1), dates1[3]);
            Assert.AreEqual(new DateTime(2015, 1, 2), dates1[4]);

            Assert.AreEqual(new DateTime(2016, 1, 4), dates2[0]);
            Assert.AreEqual(new DateTime(2016, 1, 5), dates2[1]);
            Assert.AreEqual(new DateTime(2016, 1, 6), dates2[2]);
            Assert.AreEqual(new DateTime(2016, 1, 7), dates2[3]);
            Assert.AreEqual(new DateTime(2016, 1, 8), dates2[4]);

            Assert.AreEqual(new DateTime(2017, 1, 2), dates3[0]);
            Assert.AreEqual(new DateTime(2017, 1, 3), dates3[1]);
            Assert.AreEqual(new DateTime(2017, 1, 4), dates3[2]);
            Assert.AreEqual(new DateTime(2017, 1, 5), dates3[3]);
            Assert.AreEqual(new DateTime(2017, 1, 6), dates3[4]);

            Assert.AreEqual(new DateTime(2018, 1, 1), dates4[0]);
            Assert.AreEqual(new DateTime(2018, 1, 2), dates4[1]);
            Assert.AreEqual(new DateTime(2018, 1, 3), dates4[2]);
            Assert.AreEqual(new DateTime(2018, 1, 4), dates4[3]);
            Assert.AreEqual(new DateTime(2018, 1, 5), dates4[4]);

            Assert.AreEqual(new DateTime(2016, 3, 7), dates5[0]);
            Assert.AreEqual(new DateTime(2016, 3, 8), dates5[1]);
            Assert.AreEqual(new DateTime(2016, 3, 9), dates5[2]);
            Assert.AreEqual(new DateTime(2016, 3, 10), dates5[3]);
            Assert.AreEqual(new DateTime(2016, 3, 11), dates5[4]);

            Assert.AreEqual(new DateTime(2016, 12, 26), dates6[0]);
            Assert.AreEqual(new DateTime(2016, 12, 27), dates6[1]);
            Assert.AreEqual(new DateTime(2016, 12, 28), dates6[2]);
            Assert.AreEqual(new DateTime(2016, 12, 29), dates6[3]);
            Assert.AreEqual(new DateTime(2016, 12, 30), dates6[4]);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());
            var calendarService = new CalendarService(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);

            var res1 = calendarService.IsWeekendDay(new DateTime(2016, 3, 21));
            var res2 = calendarService.IsWeekendDay(new DateTime(2016, 3, 22));
            var res3 = calendarService.IsWeekendDay(new DateTime(2016, 3, 23));
            var res4 = calendarService.IsWeekendDay(new DateTime(2016, 3, 24));
            var res5 = calendarService.IsWeekendDay(new DateTime(2016, 3, 25));
            var res6 = calendarService.IsWeekendDay(new DateTime(2016, 3, 26));
            var res7 = calendarService.IsWeekendDay(new DateTime(2016, 3, 27));

            Assert.AreEqual(false, res1);
            Assert.AreEqual(false, res2);
            Assert.AreEqual(false, res3);
            Assert.AreEqual(false, res4);
            Assert.AreEqual(false, res5);
            Assert.AreEqual(true, res6);
            Assert.AreEqual(true, res7);
        }
    }
}

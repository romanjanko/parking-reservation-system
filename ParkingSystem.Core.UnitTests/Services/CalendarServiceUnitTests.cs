using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Services;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.UnitTests.Services
{
    [TestClass]
    public class CalendarServiceUnitTests
    {
        private readonly DateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly CurrentTimeForCentralEuropeTimeZone _currentTime;
        private readonly WeekOfYearIterator _weekOfYearIterator;
        private readonly DatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;
        private readonly WeekendDayUtils _weekendDayUtils;

        public CalendarServiceUnitTests()
        {
            _dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var dayOfWeekUtils = new DayOfWeekUtils();
            _currentTime = new CurrentTimeForCentralEuropeTimeZone();
            _weekendDayUtils = new WeekendDayUtils();

            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(_dateToWeekOfYearConvertor, dayOfWeekUtils);
            _weekOfYearIterator = new WeekOfYearIterator(_dateToWeekOfYearConvertor, weekOfYearToDateConvertor,
                _currentTime);
            _datesOfBusinessDaysCounter = new DatesOfBusinessDaysCounter(_dateToWeekOfYearConvertor, weekOfYearToDateConvertor);

        }

        private CalendarService GetNewCalendarServiceInstance()
        {
            return new CalendarService(_weekOfYearIterator, _currentTime, _datesOfBusinessDaysCounter,
                _weekendDayUtils);
        }

        [TestMethod]
        public void CanGetCurrentYear()
        {
            var calendarService = GetNewCalendarServiceInstance();

            //TODO - is this a good test?
            var result = calendarService.GetCurrentWeekOfYear();

            Assert.AreEqual(_currentTime.Now().Year, result.Year);
        }

        [TestMethod]
        public void CanGetCurrentWeekInYear()
        {
            var calendarService = GetNewCalendarServiceInstance();

            //TODO - is this a good test?
            var expectedWeek = _dateToWeekOfYearConvertor.GetWeekOfYear(_currentTime.Now().Date).Week;

            Assert.AreEqual(expectedWeek, calendarService.GetCurrentWeekOfYear().Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_1()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear {Year = 2016, Week = 1});

            Assert.AreEqual(2015, result.Year);
            Assert.AreEqual(53, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_2()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2016, Week = 9 });

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(8, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_3()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2016, Week = 52 });

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(51, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_4()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2017, Week = 1 });

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_5()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2017, Week = 2 });

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_6()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2017, Week = 30 });

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(29, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_7()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2018, Week = 1 });

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetPreviousWeekInYear_8()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetPreviousWeekInYear(new WeekOfYear { Year = 2018, Week = 2 });

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_1()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear {Year = 2015, Week = 53});

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_2()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear { Year = 2016, Week = 8 });

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(9, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_3()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear {Year = 2016, Week = 51 });

            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(52, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_4()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear { Year = 2016, Week = 52 });

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_5()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear { Year = 2017, Week = 1 });

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(2, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_6()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear { Year = 2017, Week = 29 });

            Assert.AreEqual(2017, result.Year);
            Assert.AreEqual(30, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_7()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear { Year = 2017, Week = 52 });

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(1, result.Week);
        }

        [TestMethod]
        public void CanGetNextWeekInYear_8()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetNextWeekInYear(new WeekOfYear { Year = 2018, Week = 1 });

            Assert.AreEqual(2018, result.Year);
            Assert.AreEqual(2, result.Week);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek_1()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear {Year = 2015, Week = 1}).ToList();

            Assert.AreEqual(new DateTime(2014, 12, 29), result[0]);
            Assert.AreEqual(new DateTime(2014, 12, 30), result[1]);
            Assert.AreEqual(new DateTime(2014, 12, 31), result[2]);
            Assert.AreEqual(new DateTime(2015, 1, 1), result[3]);
            Assert.AreEqual(new DateTime(2015, 1, 2), result[4]);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek_2()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2016, Week = 1 }).ToList();

            Assert.AreEqual(new DateTime(2016, 1, 4), result[0]);
            Assert.AreEqual(new DateTime(2016, 1, 5), result[1]);
            Assert.AreEqual(new DateTime(2016, 1, 6), result[2]);
            Assert.AreEqual(new DateTime(2016, 1, 7), result[3]);
            Assert.AreEqual(new DateTime(2016, 1, 8), result[4]);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek_3()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2017, Week = 1 }).ToList();

            Assert.AreEqual(new DateTime(2017, 1, 2), result[0]);
            Assert.AreEqual(new DateTime(2017, 1, 3), result[1]);
            Assert.AreEqual(new DateTime(2017, 1, 4), result[2]);
            Assert.AreEqual(new DateTime(2017, 1, 5), result[3]);
            Assert.AreEqual(new DateTime(2017, 1, 6), result[4]);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek_4()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2018, Week = 1 }).ToList();

            Assert.AreEqual(new DateTime(2018, 1, 1), result[0]);
            Assert.AreEqual(new DateTime(2018, 1, 2), result[1]);
            Assert.AreEqual(new DateTime(2018, 1, 3), result[2]);
            Assert.AreEqual(new DateTime(2018, 1, 4), result[3]);
            Assert.AreEqual(new DateTime(2018, 1, 5), result[4]);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek_5()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2016, Week = 10 }).ToList();

            Assert.AreEqual(new DateTime(2016, 3, 7), result[0]);
            Assert.AreEqual(new DateTime(2016, 3, 8), result[1]);
            Assert.AreEqual(new DateTime(2016, 3, 9), result[2]);
            Assert.AreEqual(new DateTime(2016, 3, 10), result[3]);
            Assert.AreEqual(new DateTime(2016, 3, 11), result[4]);
        }

        [TestMethod]
        public void CanGetDatesOfBusinessDaysInWeek_6()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.GetDatesOfBusinessDaysInWeek(new WeekOfYear { Year = 2016, Week = 52 }).ToList();

            Assert.AreEqual(new DateTime(2016, 12, 26), result[0]);
            Assert.AreEqual(new DateTime(2016, 12, 27), result[1]);
            Assert.AreEqual(new DateTime(2016, 12, 28), result[2]);
            Assert.AreEqual(new DateTime(2016, 12, 29), result[3]);
            Assert.AreEqual(new DateTime(2016, 12, 30), result[4]);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Monday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 21));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Tuesday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 22));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Wednesday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 23));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Thursday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 24));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Friday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 25));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Saturday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 26));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanCheckIfIsWeekendDay_Sunday()
        {
            var calendarService = GetNewCalendarServiceInstance();

            var result = calendarService.IsWeekendDay(new DateTime(2016, 3, 27));

            Assert.AreEqual(true, result);
        }
    }
}

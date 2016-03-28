using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules;
using Moq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class GarageMaxTwiceWeekReservationRuleUnitTests
    {
        private readonly DateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly WeekOfYearToDateConvertor _weekOfYearToDateConvertor;
        private readonly CurrentTimeForUtcPlusTwoHoursTimeZone _currentTime;

        public GarageMaxTwiceWeekReservationRuleUnitTests()
        {
            _dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            _weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(_dateToWeekOfYearConvertor, new DayOfWeekUtils());
            _currentTime = new CurrentTimeForUtcPlusTwoHoursTimeZone();
        }

        private ParkingSpot GetGarageParkingSpot()
        {
            return new ParkingSpot
            {
                Type = ParkingSpotType.Garage
            };
        }

        private ParkingSpot GetOutsideParkingSpot()
        {
            return new ParkingSpot
            {
                Type = ParkingSpotType.Outside
            };
        }

        private ApplicationUser GetAdminUser()
        {
            return new ApplicationUser
            {
                UserName = "admin"
            };
        }

        private ApplicationUser GetRegularUser()
        {
            return new ApplicationUser
            {
                UserName = "user"
            };
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnlyTwiceInSameWeek()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetGarageReservationsByUser(It.IsAny<ApplicationUser>(),
                                                          new DateTime(2016, 3, 21),/* Monday */
                                                          new DateTime(2016, 3, 25) /* Friday */))
                .Returns(new Reservation[]
                {
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 22),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    },
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, _currentTime);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCannotReserveGarageMoreThanTwiceInSameWeek()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetGarageReservationsByUser(It.IsAny<ApplicationUser>(),
                                                          new DateTime(2016, 3, 21),/* Monday */
                                                          new DateTime(2016, 3, 25) /* Friday */))
                .Returns(new Reservation[]
                {
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 22),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    },
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 23),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    },
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, _currentTime);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservationValidationResult));
        }


        [TestMethod]
        public void RegularUserCanReserveOutsideUnlimitedlyInSameWeek()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetGarageReservationsByUser(It.IsAny<ApplicationUser>(),
                                                          new DateTime(2016, 3, 21),/* Monday */
                                                          new DateTime(2016, 3, 25) /* Friday */))
                .Returns(new Reservation[]
                {
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 22),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    },
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 23),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, _currentTime);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    ParkingSpot = GetOutsideParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanReserveGarageMoreThanTwiceInSameWeek()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetGarageReservationsByUser(It.IsAny<ApplicationUser>(),
                                                          new DateTime(2016, 3, 21),/* Monday */
                                                          new DateTime(2016, 3, 25) /* Friday */))
                .Returns(new Reservation[]
                {
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 22),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetAdminUser()
                    },
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 23),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetAdminUser()
                    },
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, _currentTime);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        //TODO add test for noon "threshold"
    }
}

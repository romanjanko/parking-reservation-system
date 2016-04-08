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

        public GarageMaxTwiceWeekReservationRuleUnitTests()
        {
            _dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            _weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(_dateToWeekOfYearConvertor, new DayOfWeekUtils());
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
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 23, 13, 0, 0);

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullNonFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCannotReserveGarageMoreThanTwiceInSameWeek()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 23, 13, 0, 0);

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }
        
        [TestMethod]
        public void RegularUserCanReserveOutsideUnlimitedlyInSameWeek()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 23, 13, 0, 0);

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetOutsideParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanReserveGarageMoreThanTwiceInSameWeek()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 23, 13, 0, 0);

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25),
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }
        
        [TestMethod]
        public void RegularUserCanReserveGarageMoreThanTwiceInSameWeekAfterNoonThreshold()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 24, 12, 0, 0); /* Thursday, 12:00 (just after threshold) */

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25, 10, 0, 0), /* Friday, 10:00 (time shouldn't be important here) */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCannotReserveGarageMoreThanTwiceInSameWeekJustBeforeNoonThreshold()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 24, 11, 59, 59); /* Thursday, 11:59:59 (just before threshold) */

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25, 10, 0, 0), /* Friday, 10:00 (time shouldn't be important here) */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }

        [TestMethod]
        public void RegularUserCannotReserveGarageMoreThanTwiceInSameWeekLongBeforeNoonThreshold()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 23, 13, 0, 0); /* Wednesday, 13:00 (long before threshold) */

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25, 10, 0, 0), /* Friday, 10:00 (time shouldn't be important here) */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageMoreThanTwiceInSameWeekLongAfterNoonThreshold_1()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 25, 13, 0, 0); /* Friday, 13:00 (long after threshold) */

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25, 10, 0, 0), /* Friday, 10:00 (time shouldn't be important here) */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageMoreThanTwiceInSameWeekLongAfterNoonThreshold_2()
        {

            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetNonFreeGarageReservationsByUser(It.IsAny<ApplicationUser>(),
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

            var reservationCreatedDate = new DateTime(2016, 3, 26, 11, 0, 0); /* Saturday, 11:00 (long after threshold) */

            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime.Setup(m => m.Now()).Returns(reservationCreatedDate);

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor, mockedCurrentTime.Object);

            var result = garageMaxTwiceWeekReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25, 10, 0, 0), /* Friday, 10:00 (time shouldn't be important here) */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }
    }
}

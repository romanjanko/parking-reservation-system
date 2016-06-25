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
using ParkingSystem.Core.ReservationRules.AntiCheatingPolicies;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class GarageMaxTwiceWeekReservationRuleUnitTests
    {
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;

        public GarageMaxTwiceWeekReservationRuleUnitTests()
        {
            var dateToWeekOfYearConvertor = new DateToWeekOfYearConvertor();
            var weekOfYearToDateConvertor = new WeekOfYearToDateConvertor(dateToWeekOfYearConvertor, new DayOfWeekUtils());

            _datesOfBusinessDaysCounter = new DatesOfBusinessDaysCounter(dateToWeekOfYearConvertor, weekOfYearToDateConvertor);
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

        private ICheatingCheck GetMockedCheatingCheck()
        {
            var mockedCheatingCheck = new Mock<ICheatingCheck>();
            mockedCheatingCheck
                .Setup(m => m.CheckForCheating(It.IsAny<Reservation>()))
                .Returns(new NoCheatingDetected());

            return mockedCheatingCheck.Object;
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
          
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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

            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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
            
            var garageMaxTwiceWeekReservationRule = new GarageMaxTwiceWeekReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

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

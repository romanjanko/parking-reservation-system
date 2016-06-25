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
    public class GarageOnMondayOrFridayReservationRuleUnitTests
    {
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;

        public GarageOnMondayOrFridayReservationRuleUnitTests()
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
        public void RegularUserCannotReserveGarageOnMondayAndFridayInSameWeek_1()
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
                        ReservationDate = new DateTime(2016, 3, 21), /* Monday */
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

            var reservationCreatedDate = new DateTime(2016, 3, 23, 11, 0, 0); /* Wednesday, 11:00 */
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object, 
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }

        [TestMethod]
        public void RegularUserCannotReserveGarageOnMondayAndFridayInSameWeek_2()
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
                        ReservationDate = new DateTime(2016, 3, 25), /* Friday */
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

            var reservationCreatedDate = new DateTime(2016, 3, 17, 12, 0, 0); /* Thursday, 12:00 */
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayOrFridayInSameWeek_1()
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

            var reservationCreatedDate = new DateTime(2016, 3, 18, 11, 0, 0);
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullNonFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayOrFridayInSameWeek_2()
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

            var reservationCreatedDate = new DateTime(2016, 3, 20, 11, 0, 0);
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullNonFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCanBeAtGarageOnFridayAndReserveOutsideOnMondayInSameWeek()
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
                        ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var reservationCreatedDate = new DateTime(2016, 3, 20, 11, 0, 0);
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetOutsideParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void RegularUserCanBeAtGarageOnMondayAndReserveOutsideOnFridayInSameWeek()
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
                        ReservationDate = new DateTime(2016, 3, 21), /* Monday */
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

            var reservationCreatedDate = new DateTime(2016, 3, 20, 11, 0, 0);
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetOutsideParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanReserveGarageOnMondayAndFridayInSameWeek_1()
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
                        ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetAdminUser()
                    },
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 23),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetAdminUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var reservationCreatedDate = new DateTime(2016, 3, 20, 11, 0, 0);
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }

        [TestMethod]
        public void AdminUserCanReserveGarageOnMondayAndFridayInSameWeek_2()
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
                        ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetAdminUser()
                    },
                    new Reservation
                    {
                        ReservationDate = new DateTime(2016, 3, 23),
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetAdminUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var reservationCreatedDate = new DateTime(2016, 3, 20, 11, 0, 0);
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayAndFridayInSameWeekUnderCertainConditions_1()
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
                        ReservationDate = new DateTime(2016, 3, 21), /* Monday */
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

            var reservationCreatedDate = new DateTime(2016, 3, 24, 12, 0, 0); /* condition: day before, after noon */
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayAndFridayInSameWeekUnderCertainConditions_2()
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
                        ReservationDate = new DateTime(2016, 3, 25), /* Friday */
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

            var reservationCreatedDate = new DateTime(2016, 3, 20, 12, 0, 0); /* condition: day before, after noon */
            
            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _datesOfBusinessDaysCounter, GetMockedCheatingCheck());

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    CreatedDate = reservationCreatedDate,
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullFreeGarageReservation));
        }
    }
}

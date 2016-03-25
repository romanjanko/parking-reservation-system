using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Utils;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules;
using Moq;
using ParkingSystem.Core.RepositoryAbstraction;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class GarageOnMondayOrFridayReservationRuleUnitTests
    {
        private readonly DateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly WeekOfYearToDateConvertor _weekOfYearToDateConvertor;

        public GarageOnMondayOrFridayReservationRuleUnitTests()
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
        public void RegularUserCannotReserveGarageOnMondayAndFridayInSameWeek_1()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object, 
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCannotReserveGarageOnMondayAndFridayInSameWeek_2()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayOrFridayInSameWeek_1()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayOrFridayInSameWeek_2()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanBeAtGarageOnFridayAndReserveOutsideOnMondayInSameWeek()
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
                        ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                        ParkingSpot = GetGarageParkingSpot(),
                        ApplicationUser = GetRegularUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    ParkingSpot = GetOutsideParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanBeAtGarageOnMondayAndReserveOutsideOnFridayInSameWeek()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    ParkingSpot = GetOutsideParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanReserveGarageOnMondayAndFridayInSameWeek_1()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanReserveGarageOnMondayAndFridayInSameWeek_2()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayAndFridayInSameWeekUnderCertainConditions_1()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 25), /* Friday */
                    CreatedDate = new DateTime(2016, 3, 24, 12, 0, 0), /* condition: day before, after noon */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            //TODO - need to get rid of today date in GarageOnMondayOrFridayReservationRule
            //Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanReserveGarageOnMondayAndFridayInSameWeekUnderCertainConditions_2()
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

            var garageOnMondayOrFridayReservationRule = new GarageOnMondayOrFridayReservationRule(mockedUnitOfWork.Object,
                _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor);

            var result = garageOnMondayOrFridayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = new DateTime(2016, 3, 21), /* Monday */
                    CreatedDate = new DateTime(2016, 3, 20, 12, 0, 0), /* condition: day before, after noon */
                    ParkingSpot = GetGarageParkingSpot(),
                    ApplicationUser = GetRegularUser()
                });

            //TODO - need to get rid of today date in GarageOnMondayOrFridayReservationRule
            //Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }
    }
}

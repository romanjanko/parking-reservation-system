using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.Time;
using Moq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules.AntiCheatingPolicies;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class CheatingCheckUnitTests
    {
        private ICurrentTime GetMockedCurrentTime(DateTime fakeTime)
        {
            var mockedCurrentTime = new Mock<ICurrentTime>();
            mockedCurrentTime
                .Setup(m => m.Now())
                .Returns(fakeTime);

            return mockedCurrentTime.Object;
        }

        private IBusinessDaysIterator GetBusinessDaysIterator()
        {
            return new BusinessDaysIterator(new WeekendDayUtils());
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
        public void OnlyReservationsForTodayOrNextBusinessDayDateShouldBeLogged_1()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 17);              // Friday
            var nextBusinessDayDate = new DateTime(2016, 6, 20);    // Monday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate     // reservation for Friday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Once);
        }
        
        [TestMethod]
        public void OnlyReservationsForTodayOrNextBusinessDayDateShouldBeLogged_2()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 20);              // Monday
            var nextBusinessDayDate = new DateTime(2016, 6, 21);    // Tuesday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate     // reservation for Monday
            };
            
            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Once);
        }

        [TestMethod]
        public void OnlyReservationsForTodayOrNextBusinessDayDateShouldBeLogged_3()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 17);              // Friday
            var nextBusinessDayDate = new DateTime(2016, 6, 20);    // Monday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = nextBusinessDayDate     // reservation for Monday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Once);
        }

        [TestMethod]
        public void OnlyReservationsForTodayOrNextBusinessDayDateShouldBeLogged_4()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 18);              // Saturday
            var nextBusinessDayDate = new DateTime(2016, 6, 20);    // Monday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = nextBusinessDayDate     // reservation for Monday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Once);
        }

        [TestMethod]
        public void OnlyReservationsForTodayOrNextBusinessDayDateShouldBeLogged_5()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 20);              // Monday
            var nextBusinessDayDate = new DateTime(2016, 6, 21);    // Tuesday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = nextBusinessDayDate     // reservation for Tuesday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Once);
        }

        [TestMethod]
        public void OnlyReservationsForTodayOrNextBusinessDayDateShouldBeLogged_6()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 22);              // Wednesday
            var nextBusinessDayDate = new DateTime(2016, 6, 23);    // Thursday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = nextBusinessDayDate     // reservation for Thursday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Once);
        }

        [TestMethod]
        public void ReservationsForTheDayAfterNextBusinessDayDateShouldNotBeLogged()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 17);              // Friday
            var nextBusinessDayDate = new DateTime(2016, 6, 20);    // Monday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = nextBusinessDayDate.AddDays(1)   // reservation for Tuesday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Never);
        }

        [TestMethod]
        public void ReservationsForYesterdayShouldNotBeLogged()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 22);              // Wednesday
            var nextBusinessDayDate = new DateTime(2016, 6, 23);    // Thursday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate.AddDays(-1)   // reservation for Tuesday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Never);
        }

        [TestMethod]
        public void ReservationsForOutsideShouldNotBeLogged()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 22);              // Wednesday
            var nextBusinessDayDate = new DateTime(2016, 6, 23);    // Thursday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Outside }, // Outside parking spot, not Garage
                ReservationDate = todayDate     // reservation for Wednesday
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetRegularUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Never);
        }

        [TestMethod]
        public void ReservationsDeletedByAdminShouldNotBeLogged()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();
            mockedDeletedReservationRepository.Setup(m => m.Add(It.IsAny<DeletedReservation>()));

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var todayDate = new DateTime(2016, 6, 22);              // Wednesday
            var nextBusinessDayDate = new DateTime(2016, 6, 23);    // Thursday

            var reservationToDelete = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,     // reservation for Wednesday
                ApplicationUser = GetRegularUser()
            };

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.Log(reservationToDelete, GetAdminUser());

            mockedDeletedReservationRepository.Verify(m => m.Add(It.IsAny<DeletedReservation>()), Times.Never);
        }

        [TestMethod]
        public void CheckForCheating_1()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 17);    // Friday
            var todayDate = new DateTime(2016, 6, 20);                  // Monday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-3), todayDate))
                .Returns(new DeletedReservation[]
                {
                    // no previous reservations
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(NoCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheating_2()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 17);    // Friday
            var todayDate = new DateTime(2016, 6, 20);                  // Monday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-3), todayDate))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(PossibleCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheating_3()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 20);    // Monday
            var todayDate = new DateTime(2016, 6, 21);                  // Tuesday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-1), todayDate))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(PossibleCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheating_4()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 20);    // Monday
            var todayDate = new DateTime(2016, 6, 21);                  // Tuesday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-1), todayDate))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = true
                    },
                    new DeletedReservation
                    {
                        ReservedFreely = true
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(NoCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheating_5()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 17);    // Friday
            var todayDate = new DateTime(2016, 6, 18);                  // Saturday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = new DateTime(2016, 6, 20),            // Monday
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-1), todayDate.AddDays(2)))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = true
                    },
                    new DeletedReservation
                    {
                        ReservedFreely = true
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(NoCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheating_6()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 17);    // Friday
            var todayDate = new DateTime(2016, 6, 19);                  // Sunday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = new DateTime(2016, 6, 20),            // Monday
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-2), todayDate.AddDays(1)))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(PossibleCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheating_7()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 20);    // Monday
            var todayDate = new DateTime(2016, 6, 21);                  // Tuesday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,
                ApplicationUser = GetRegularUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-1), todayDate))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                    new DeletedReservation
                    {
                        ReservedFreely = true
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(NoCheatingDetected), result.GetType());
        }

        [TestMethod]
        public void CheckForCheatingDoesNotApplyForAdmin()
        {
            var mockedDeletedReservationRepository = new Mock<IDeletedReservationRepository>();

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.DeletedReservations).Returns(mockedDeletedReservationRepository.Object);

            var previousBusinessDayDate = new DateTime(2016, 6, 20);    // Monday
            var todayDate = new DateTime(2016, 6, 21);                  // Tuesday

            var newReservation = new Reservation
            {
                ParkingSpot = new ParkingSpot { Type = ParkingSpotType.Garage },
                ReservationDate = todayDate,
                ApplicationUser = GetAdminUser()
            };

            mockedDeletedReservationRepository
                .Setup(m => m.GetDeletedReservations(It.IsAny<ApplicationUser>(), newReservation.ReservationDate,
                                                     todayDate.AddDays(-1), todayDate))
                .Returns(new DeletedReservation[]
                {
                    // previously created reservations for today or previous business day
                    new DeletedReservation
                    {
                        ReservedFreely = false
                    },
                });

            var cheatingCheck = new CheatingCheck(mockedUnitOfWork.Object, GetMockedCurrentTime(todayDate), GetBusinessDaysIterator());
            cheatingCheck.CheckForCheating(newReservation);

            var result = cheatingCheck.CheckForCheating(newReservation);

            Assert.AreEqual(typeof(NoCheatingDetected), result.GetType());
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.RepositoryAbstraction;
using Moq;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class OneParkingSpotPerDayReservationRuleUnitTests
    {
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
        public void RegularUserCannotReserveMoreParkingSpotsInOneDay()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetAllReservationsByUser(It.IsAny<ApplicationUser>(), DateTime.Today.Date, DateTime.Today.Date))
                .Returns(new Reservation[]
                {
                    new Reservation
                    {
                        ReservationDate = DateTime.Today.Date,
                        ParkingSpot = new ParkingSpot { Name = "ps1", Type = ParkingSpotType.Outside },
                        ApplicationUser = GetRegularUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var oneParkingSpotPerDayReservationRule = new OneParkingSpotPerDayReservationRule(mockedUnitOfWork.Object);

            var result = oneParkingSpotPerDayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = DateTime.Today.Date,
                    CreatedDate = DateTime.Today.Date,
                    ParkingSpot = new ParkingSpot { Name = "ps2", Type = ParkingSpotType.Garage },
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanReserveOnlyOneParkingSpotsInOneDay()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetAllReservationsByUser(It.IsAny<ApplicationUser>(), DateTime.Today.Date, DateTime.Today.Date))
                .Returns(new Reservation[] { });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var oneParkingSpotPerDayReservationRule = new OneParkingSpotPerDayReservationRule(mockedUnitOfWork.Object);

            var result = oneParkingSpotPerDayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = DateTime.Today.Date,
                    CreatedDate = DateTime.Today.Date,
                    ParkingSpot = new ParkingSpot { Name = "ps2", Type = ParkingSpotType.Garage },
                    ApplicationUser = GetRegularUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanReserveMoreParkingSpotsInOneDay()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository
                .Setup(m => m.GetAllReservationsByUser(It.IsAny<ApplicationUser>(), DateTime.Today.Date, DateTime.Today.Date))
                .Returns(new Reservation[]
                {
                    new Reservation
                    {
                        ReservationDate = DateTime.Today.Date,
                        ParkingSpot = new ParkingSpot { Name = "ps1", Type = ParkingSpotType.Outside },
                        ApplicationUser = GetAdminUser()
                    }
                });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var oneParkingSpotPerDayReservationRule = new OneParkingSpotPerDayReservationRule(mockedUnitOfWork.Object);

            var result = oneParkingSpotPerDayReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = DateTime.Today.Date,
                    CreatedDate = DateTime.Today.Date,
                    ParkingSpot = new ParkingSpot { Name = "ps2", Type = ParkingSpotType.Garage },
                    ApplicationUser = GetAdminUser()
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.Core.ReservationRules;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class OnlyFreeParkingSpotReservationRuleUnitTests
    {
        [TestMethod]
        public void OnlyFreeParkingSpotCanBeReserved()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository.Setup(m => m.GetReservation(It.IsAny<ParkingSpot>(), It.IsAny<DateTime>()))
                .Returns((Reservation) null);

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var onlyFreeParkingSpotReservationRule = new OnlyFreeParkingSpotReservationRule(mockedUnitOfWork.Object);

            var result = onlyFreeParkingSpotReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = DateTime.Today.Date,
                    ParkingSpot = new ParkingSpot { Name = "ps1", Type = ParkingSpotType.Garage }
                });

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AlreadyReservedParkingSpotCanBeReservedAgain()
        {
            var mockedReservationRepository = new Mock<IReservationRepository>();
            mockedReservationRepository.Setup(m => m.GetReservation(It.IsAny<ParkingSpot>(), It.IsAny<DateTime>()))
                .Returns(new Reservation { });

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            mockedUnitOfWork.Setup(m => m.Reservations).Returns(mockedReservationRepository.Object);

            var onlyFreeParkingSpotReservationRule = new OnlyFreeParkingSpotReservationRule(mockedUnitOfWork.Object);

            var result = onlyFreeParkingSpotReservationRule.Validate(
                new Reservation
                {
                    ReservationDate = DateTime.Today.Date,
                    ParkingSpot = new ParkingSpot { Name = "ps1", Type = ParkingSpotType.Garage }
                });

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }
    }
}

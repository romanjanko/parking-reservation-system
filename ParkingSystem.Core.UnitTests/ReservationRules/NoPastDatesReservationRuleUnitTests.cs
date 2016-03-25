using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class NoPastDatesReservationRuleUnitTests
    {
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
        public void RegularUserCanMakeReservationsForDatesEqualsToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanMakeReservationsForDatesEqualsToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesEqualsToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesEqualsToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanMakeReservationsForDatesGreaterToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCanMakeReservationsForDatesGreaterToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesGreaterToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesGreaterToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCannotMakeReservationsForPastDates_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(FailedReservationValidationResult));
        }

        [TestMethod]
        public void RegularUserCannotMakeReservationsForPastDates_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(FailedReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForPastDates_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForPastDates_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule();

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullReservationValidationResult));
        }
    }
}

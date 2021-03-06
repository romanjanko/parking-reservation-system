﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules;
using ParkingSystem.Core.Time;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class NoPastDatesReservationRuleUnitTests
    {
        private readonly CurrentTimeForCentralEuropeTimeZone _currentTime;

        public NoPastDatesReservationRuleUnitTests()
        {
            //TODO maybe it would be better to remove it - also, it depends on current implementation which is
            //wrong approach.
            _currentTime = new CurrentTimeForCentralEuropeTimeZone();
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
        public void RegularUserCanMakeReservationsForDatesEqualsToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void RegularUserCanMakeReservationsForDatesEqualsToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesEqualsToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesEqualsToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today,
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void RegularUserCanMakeReservationsForDatesGreaterToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void RegularUserCanMakeReservationsForDatesGreaterToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesGreaterToday_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForDatesGreaterToday_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void RegularUserCannotMakeReservationsForPastDates_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }

        [TestMethod]
        public void RegularUserCannotMakeReservationsForPastDates_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetRegularUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(FailedReservation));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForPastDates_Garage()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetGarageParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }

        [TestMethod]
        public void AdminUserCanMakeReservationsForPastDates_Outside()
        {
            var onlyFutureDatesReservationRule = new NoPastDatesReservationRule(_currentTime);

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Today.AddDays(-1),
                CreatedDate = DateTime.Today,
                ApplicationUser = GetAdminUser(),
                ParkingSpot = GetOutsideParkingSpot()
            };

            var result = onlyFutureDatesReservationRule.Validate(reservation);

            Assert.IsInstanceOfType(result, typeof(SuccessfullCommonReservation));
        }
    }
}

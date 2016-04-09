using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingSystem.Core.ReservationRules;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.UnitTests.ReservationRules
{
    [TestClass]
    public class AbstractReservationRuleUnitTests
    {
        private class ReservationRule : AbstractReservationRule
        {
            public override ReservationValidationResult Validate(Reservation reservation)
            {
                throw new NotImplementedException();
            }
        }

        private AbstractReservationRule GetAbstractReservationRuleInstance()
        {
            return new ReservationRule();
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_1()
        {
            var reservationRule = GetAbstractReservationRuleInstance();
            
            var createdDateTime = new DateTime(2016, 4, 6, 13, 0, 0);
            var reservationDate = new DateTime(2016, 4, 8); // Friday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_2()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 1, 13, 0, 0);
            var reservationDate = new DateTime(2016, 4, 8); // Friday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_3()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 7, 11, 0, 0);
            var reservationDate = new DateTime(2016, 4, 8); // Friday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_4()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 7, 12, 0, 0);
            var reservationDate = new DateTime(2016, 4, 8); // Friday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_5()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 8, 11, 0, 0);
            var reservationDate = new DateTime(2016, 4, 8); // Friday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_6()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 9, 11, 0, 0);
            var reservationDate = new DateTime(2016, 4, 8); // Friday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_7()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 7, 12, 0, 0);
            var reservationDate = new DateTime(2016, 4, 11); // Monday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_8()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 8, 11, 0, 0); // Friday
            var reservationDate = new DateTime(2016, 4, 11); // Monday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_9()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 8, 12, 0, 0); // Friday
            var reservationDate = new DateTime(2016, 4, 11); // Monday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_10()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 9, 11, 0, 0); // Saturday
            var reservationDate = new DateTime(2016, 4, 11); // Monday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_11()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 10, 11, 0, 0); // Sunday
            var reservationDate = new DateTime(2016, 4, 11); // Monday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsRightTimeToRemoveAllRestrictions_12()
        {
            var reservationRule = GetAbstractReservationRuleInstance();

            var createdDateTime = new DateTime(2016, 4, 11, 11, 0, 0); // Monday
            var reservationDate = new DateTime(2016, 4, 11); // Monday

            var result = reservationRule.IsRightTimeToRemoveAllRestrictions(
                new Reservation
                {
                    ReservationDate = reservationDate,
                    CreatedDate = createdDateTime
                });

            Assert.AreEqual(true, result);
        }
    }
}

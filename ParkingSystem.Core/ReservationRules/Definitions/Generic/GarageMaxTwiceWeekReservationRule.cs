using System;
using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules.AntiCheatingPolicies;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageMaxTwiceWeekReservationRule : AbstractReservationRule
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;
        private readonly ICheatingCheck _cheatingCheck;

        public GarageMaxTwiceWeekReservationRule(IUnitOfWork unitOfWork,
                                                 IDatesOfBusinessDaysCounter datesOfBusinessDaysCounter,
                                                 ICheatingCheck cheatingCheck)
            : base()
        {
            _unitOfWork = unitOfWork;
            _datesOfBusinessDaysCounter = datesOfBusinessDaysCounter;
            _cheatingCheck = cheatingCheck;

        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationMadeForOutsideParkingSpot(reservation))
                return new SuccessfullCommonReservation();

            if (IsReservationMadeByAdminUser(reservation))
                return new SuccessfullFreeGarageReservation();

            var cheatingCheckResult = _cheatingCheck.CheckForCheating(reservation);
            var rightTimeRemoveRestrictions = IsRightTimeToRemoveAllRestrictions(reservation);

            if (rightTimeRemoveRestrictions && 
                cheatingCheckResult.CheatingDetected == false)
                return new SuccessfullFreeGarageReservation();

            var userInGarageCount = GetUserGarageUsageInWeek(reservation.ApplicationUser, reservation.ReservationDate);

            if (userInGarageCount < GarageLimitPerWeek)
                return new SuccessfullNonFreeGarageReservation();
            else if (rightTimeRemoveRestrictions && cheatingCheckResult.CheatingDetected)
                return new FailedReservation(@"Possible cheating detected based on your previous reservations for this day. 
                    You cannot reserve the parking spot freely.");
            
            return new FailedReservation(@"The limit for signing up at garage parking spot 
                                               has been reached for this week.");
        }

        private int GetUserGarageUsageInWeek(ApplicationUser user, DateTime dateOfDayInWeek)
        {            
            var datesOfBusinessDaysInWeek = _datesOfBusinessDaysCounter.GetDatesOfBusinessDaysInWeek(dateOfDayInWeek);

            return _unitOfWork.Reservations
                .GetNonFreeGarageReservationsByUser(user, datesOfBusinessDaysInWeek.Monday, datesOfBusinessDaysInWeek.Friday)
                .Count();
        }
    }
}

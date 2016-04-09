using System;
using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageMaxTwiceWeekReservationRule : AbstractReservationRule
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;

        public GarageMaxTwiceWeekReservationRule(IUnitOfWork unitOfWork,
                                                 IDatesOfBusinessDaysCounter datesOfBusinessDaysCounter)
            : base()
        {
            _unitOfWork = unitOfWork;
            _datesOfBusinessDaysCounter = datesOfBusinessDaysCounter;
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationMadeForOutsideParkingSpot(reservation))
                return new SuccessfullCommonReservation();

            if (IsRightTimeToRemoveAllRestrictions(reservation) ||
                IsReservationMadeByAdminUser(reservation))
                return new SuccessfullFreeGarageReservation();
            
            var userInGarageCount = GetUserGarageUsageInWeek(reservation.ApplicationUser, reservation.ReservationDate);
            
            if (userInGarageCount < GarageLimitPerWeek)
                return new SuccessfullNonFreeGarageReservation();
            else
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

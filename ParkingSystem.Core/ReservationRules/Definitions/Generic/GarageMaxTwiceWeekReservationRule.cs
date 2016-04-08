using System;
using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageMaxTwiceWeekReservationRule : AbstractReservationRule
    {
        public GarageMaxTwiceWeekReservationRule(IUnitOfWork unitOfWork,
                                                 IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                                 IWeekOfYearToDateConvertor weekOfYearToDateConvertor,
                                                 ICurrentTime currentTime)
            : base(unitOfWork, dateToWeekOfYearConvertor, weekOfYearToDateConvertor, currentTime)
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsRightTimeToRemoveAllRestrictions(reservation) ||
                IsReservationMadeByAdminUser(reservation))
                return new SuccessfullFreeGarageReservation();

            if (IsReservationMadeForOutsideParkingSpot(reservation))
                return new SuccessfullCommonReservation();

            var userInGarageCount = GetUserGarageUsageInWeek(reservation.ApplicationUser, reservation.ReservationDate);
            
            if (userInGarageCount < GarageLimitPerWeek)
                return new SuccessfullNonFreeGarageReservation();
            else
                return new FailedReservation(@"The limit for signing up at garage parking spot 
                                                               has been reached for this week.");
        }

        private int GetUserGarageUsageInWeek(ApplicationUser user, DateTime dateOfDayInWeek)
        {
            DateTime startDateOfWeek, endDateOfWeek;

            GetStartAndEndBusinessDayOfWeek(dateOfDayInWeek, out startDateOfWeek, out endDateOfWeek);

            return UnitOfWork.Reservations
                .GetNonFreeGarageReservationsByUser(user, startDateOfWeek, endDateOfWeek)
                .Count();
        }
    }
}

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
                IsReservationMadeForOutsideParkingSpot(reservation) ||
                IsReservationMadeByAdminUser(reservation))
                return new SuccessfullReservationValidationResult();

            var userInGarageCount = GetUserGarageUsageInWeek(reservation.ApplicationUser, reservation.ReservationDate);
            
            if (userInGarageCount < GarageLimitPerWeek)
                return new SuccessfullReservationValidationResult();
            else
                return new FailedReservationValidationResult(@"The limit for signing up at garage parking spot 
                                                               has been reached for this week.");
        }

        private int GetUserGarageUsageInWeek(ApplicationUser user, DateTime dateOfDayInWeek)
        {
            DateTime startDateOfWeek, endDateOfWeek;

            GetStartAndEndBusinessDayOfWeek(dateOfDayInWeek, out startDateOfWeek, out endDateOfWeek);

            return UnitOfWork.Reservations
                .GetGarageReservationsByUser(user, startDateOfWeek, endDateOfWeek)
                .Count();
        }
    }
}

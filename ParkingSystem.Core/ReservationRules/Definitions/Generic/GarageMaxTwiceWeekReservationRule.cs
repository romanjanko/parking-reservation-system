using ParkingSystem.Core.RepositoryAbstraction;
using System;
using System.Linq;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.Utils;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageMaxTwiceWeekReservationRule : AbstractReservationRule
    {
        public GarageMaxTwiceWeekReservationRule(IUnitOfWork unitOfWork,
                                                 IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                                 IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
            : base(unitOfWork, dateToWeekOfYearConvertor, weekOfYearToDateConvertor)
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsRightTimeToRemoveAllRestrictions(reservation) ||
                IsReservationBeingMadeForOutsideParkingSpot(reservation) ||
                IsReservationBeingMadeByAdminUser(reservation))
                return new SuccessfullReservationValidationResult();

            var userInGarageCount = GetUserGarageUsageInWeek(reservation.ApplicationUser, reservation.ReservationDate);
            
            if (userInGarageCount < _garageLimitPerWeek)
                return new SuccessfullReservationValidationResult();
            else
                return new FailedReservationValidationResult(@"The limit for signing up at garage parking spot 
                                                               has been reached for this week.");
        }

        private int GetUserGarageUsageInWeek(ApplicationUser user, DateTime dateOfDayInWeek)
        {
            DateTime startDateOfWeek, endDateOfWeek;

            GetStartAndEndBusinessDayOfWeek(dateOfDayInWeek, out startDateOfWeek, out endDateOfWeek);

            return _unitOfWork.Reservations
                .GetGarageReservationsByUser(user, startDateOfWeek, endDateOfWeek)
                .Count();
        }
    }
}

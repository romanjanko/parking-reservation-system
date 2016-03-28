using System;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public abstract class AbstractReservationRule : IReservationRule
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDateToWeekOfYearConvertor DateToWeekOfYearConvertor;
        protected readonly IWeekOfYearToDateConvertor WeekOfYearToDateConvertor;
        protected readonly ICurrentTime CurrentTime;
        protected readonly DateTime NoonThreshold;
        protected readonly int GarageLimitPerWeek;

        protected AbstractReservationRule(IUnitOfWork unitOfWork,
                                          IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                          IWeekOfYearToDateConvertor weekOfYearToDateConvertor,
                                          ICurrentTime currentTime)
        {
            UnitOfWork = unitOfWork;
            DateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            WeekOfYearToDateConvertor = weekOfYearToDateConvertor;
            CurrentTime = currentTime;

            if (currentTime != null)
                NoonThreshold = new DateTime(currentTime.Now().Year, currentTime.Now().Month, currentTime.Now().Day, 12, 0, 0);

            GarageLimitPerWeek = 2;
        }
        
        public abstract ReservationValidationResult Validate(Reservation reservation);

        protected bool IsRightTimeToRemoveAllRestrictions(Reservation reservation)
        {
            return reservation.ReservationDate == CurrentTime.Now().AddDays(1) &&
                   reservation.CreatedDate >= NoonThreshold;
        }

        protected bool IsReservationBeingMadeForOutsideParkingSpot(Reservation reservation)
        {
            return reservation.ParkingSpot.Type == ParkingSpotType.Outside;
        }

        protected bool IsReservationBeingMadeByAdminUser(Reservation reservation)
        {
            // TODO rework it for roles instead of hardcoded user name
            return string.Compare(reservation.ApplicationUser.UserName, "admin", StringComparison.Ordinal) == 0;
        }

        // TODO clean and move to different class
        protected void GetStartAndEndBusinessDayOfWeek(DateTime dateOfDayInWeek, out DateTime startDate, out DateTime endDate)
        {
            if (DateToWeekOfYearConvertor == null || WeekOfYearToDateConvertor == null)
                throw new InvalidOperationException();

            var weekOfYear = DateToWeekOfYearConvertor.GetWeekOfYear(dateOfDayInWeek);

            startDate = WeekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Monday);
            endDate = WeekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Friday);
        }
    }
}

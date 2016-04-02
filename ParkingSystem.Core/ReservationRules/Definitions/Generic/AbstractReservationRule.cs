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
        protected readonly TimeSpan NoonThreshold;
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
            NoonThreshold = new TimeSpan(12, 0, 0);
            GarageLimitPerWeek = 2;
        }
        
        public abstract ReservationValidationResult Validate(Reservation reservation);

        protected bool IsRightTimeToRemoveAllRestrictions(Reservation reservation)
        {
            //TODO write unit test for it
            var shiftedReservationDate = new DateTime(reservation.ReservationDate.Year, 
                                                      reservation.ReservationDate.Month, 
                                                      reservation.ReservationDate.Day,
                                                      NoonThreshold.Hours, 
                                                      NoonThreshold.Minutes, 
                                                      NoonThreshold.Seconds).AddDays(-1);

            return reservation.CreatedDate >= shiftedReservationDate;
        }
        
        protected bool IsReservationMadeForOutsideParkingSpot(Reservation reservation)
        {
            return reservation.ParkingSpot.Type == ParkingSpotType.Outside;
        }

        protected bool IsReservationMadeByAdminUser(Reservation reservation)
        {
            // TODO rework it for roles instead of hardcoded user name
            return string.Compare(reservation.ApplicationUser.UserName, "admin", StringComparison.Ordinal) == 0;
        }

        // TODO clean and move to different class, get rid of out keyword
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

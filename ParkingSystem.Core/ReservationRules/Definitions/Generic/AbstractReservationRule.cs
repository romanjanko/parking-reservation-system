using ParkingSystem.Core.RepositoryAbstraction;
using System;
using ParkingSystem.Core.Utils;
using ParkingSystem.DomainModel.Models;
using System.Linq;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public abstract class AbstractReservationRule : IReservationRule
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        protected readonly IWeekOfYearToDateConvertor _weekOfYearToDateConvertor;
        protected readonly DateTime _noonThreshold;
        protected readonly int _garageLimitPerWeek;

        public AbstractReservationRule(IUnitOfWork unitOfWork,
                                       IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                       IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
        {
            _unitOfWork = unitOfWork;
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            _weekOfYearToDateConvertor = weekOfYearToDateConvertor;
            _noonThreshold = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);
            _garageLimitPerWeek = 2;
        }

        public AbstractReservationRule(IUnitOfWork unitOfWork)
            : this(unitOfWork, null, null)
        {
        }

        public AbstractReservationRule()
            : this(null, null, null)
        {
        }

        public abstract ReservationValidationResult Validate(Reservation reservation);

        protected bool IsRightTimeToRemoveAllRestrictions(Reservation reservation)
        {
            // TODO get rid of DateTime.Today, it creates problem with unit testing
            return reservation.ReservationDate == DateTime.Today.AddDays(1) &&
                   reservation.CreatedDate >= _noonThreshold;
        }

        protected bool IsReservationBeingMadeForOutsideParkingSpot(Reservation reservation)
        {
            return reservation.ParkingSpot.Type == ParkingSpotType.Outside;
        }

        protected bool IsReservationBeingMadeByAdminUser(Reservation reservation)
        {
            // TODO rework it for roles instead of hardcoded user name
            return reservation.ApplicationUser.UserName.CompareTo("admin") == 0;
        }

        // TODO clean and move to different class
        protected void GetStartAndEndBusinessDayOfWeek(DateTime dateOfDayInWeek, out DateTime startDate, out DateTime endDate)
        {
            if (_dateToWeekOfYearConvertor == null || _weekOfYearToDateConvertor == null)
                throw new InvalidOperationException();

            int year, week;

            _dateToWeekOfYearConvertor.GetWeekOfYear(dateOfDayInWeek, out year, out week);

            startDate = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(year, week, DayOfWeek.Monday);
            endDate = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(year, week, DayOfWeek.Friday);
        }
    }
}

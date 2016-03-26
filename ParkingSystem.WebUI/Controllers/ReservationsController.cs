using ParkingSystem.Core.Services;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.WebUI.Models;
using System;
using System.Web.Mvc;
using ParkingSystem.WebUI.Identity;
using ParkingSystem.Core.Models;

namespace ParkingSystem.WebUI.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IReservationService _reservationService;
        private readonly ICalendarService _calendarService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly int pageSize = 8;

        public ReservationsController(IParkingSpotService parkingSpotService, 
                                      IReservationService reservationService, 
                                      ICalendarService calendarService,
                                      ApplicationUserManager applicationUserManager)
        {
            _parkingSpotService = parkingSpotService;
            _reservationService = reservationService;
            _calendarService = calendarService;
            _applicationUserManager = applicationUserManager;
        }

        public ActionResult Calendar(int? year, int? week)
        { 
            var currentWeekOfYear = GetWeekOfYearToDisplay(year, week);

            int previousWeekInYear, previousYear, nextWeekInYear, nextYear;

            _calendarService.GetPreviousWeekInYear(currentWeekOfYear.Year, currentWeekOfYear.Week, 
                out previousYear, out previousWeekInYear);
            _calendarService.GetNextWeekInYear(currentWeekOfYear.Year, currentWeekOfYear.Week, 
                out nextYear, out nextWeekInYear);

            var businessDatesInCurrentWeek = _calendarService.GetDatesOfBusinessDaysInWeek(currentWeekOfYear);

            return View(
                new ReservationsListViewModel
                {
                    BusinessDatesInWeek = businessDatesInCurrentWeek,
                    ParkingSpots = _parkingSpotService.GetAllParkingSpots(),
                    Reservations = _reservationService.GetAllReservationsForDateRange(businessDatesInCurrentWeek),
                    PreviousWeekUrl = Url.Action("Calendar", new { year = previousYear, week = previousWeekInYear }),
                    NextWeekUrl = Url.Action("Calendar", new { year = nextYear, week = nextWeekInYear })
                });
        }

        private WeekOfYear GetWeekOfYearToDisplay(int? year, int? week)
        {
            int yearToDisplay, weekInYearToDisplay;

            if (year == null && week == null)
            {
                yearToDisplay = _calendarService.GetCurrentYear();
                weekInYearToDisplay = _calendarService.GetCurrentWeekInYear();

                var today = _calendarService.GetTodayDate();

                if (_calendarService.IsWeekendDay(today))
                {
                    _calendarService.GetNextWeekInYear(yearToDisplay, weekInYearToDisplay,
                        out yearToDisplay, out weekInYearToDisplay);
                }
            }
            else if (year != null && week != null)
            {
                yearToDisplay = year.Value;
                weekInYearToDisplay = week.Value;
            }
            else
            {
                throw new InvalidOperationException();
            }

            return new WeekOfYear
            {
                Week = weekInYearToDisplay,
                Year = yearToDisplay
            };
        }

        public ActionResult UserReservations(int page = 1)
        {
            var loggedApplicationUser = _applicationUserManager.FindByNameAsync(User.Identity.Name).Result;
            var pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = pageSize };

            var reservations = _reservationService.GetAllReservationsForUser(
                pagingInfo, loggedApplicationUser, DateTime.Today);

            return View(
                new UserReservationsListViewModel
                {
                    Reservations = reservations.CurrentReservations,
                    PagingInfo = reservations.PagingInfo
                });
        }

        public ActionResult AddReservation(int parkingSpotId, DateTime date)
        {
            return PartialView("_AddReservation",
                new Reservation
                {
                    ParkingSpot = _parkingSpotService.GetParkingSpot(parkingSpotId),
                    ReservationDate = date
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReservation(Reservation reservation)
        {
            var parkingSpot = _parkingSpotService.GetParkingSpot(reservation.ParkingSpot.Id);

            var reservationResult = _reservationService.AddReservation(
                new Reservation
                {
                    ParkingSpot = parkingSpot,
                    ApplicationUser = _applicationUserManager.FindByNameAsync(User.Identity.Name).Result,
                    ReservationDate = reservation.ReservationDate
                });

            if (reservationResult.Success)
            {
                TempData["messageSuccess"] = string.Format(
                    "Your reservation for parking spot {0} on {1:dddd, M/d/yyyy} has been successfully created.",
                    parkingSpot.Name, reservation.ReservationDate);

                return Json(new { success = true });
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", reservationResult.ErrorMessage);
            }
            
            return PartialView("_AddReservation", reservation);
        }

        public ActionResult DeleteReservation(int id)
        {
            var reservation = _reservationService.GetReservation(id);

            if (reservation == null)
                return View("Error");

            return PartialView("_DeleteReservation", reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReservation(Reservation reservation)
        {
            var reservationToDelete = _reservationService.GetReservation(reservation.Id);

            if (reservationToDelete == null)
                return View("Error");

            TempData["messageSuccess"] = string.Format(
                "The reservation for parking spot {0} on {1:dddd, M/d/yyyy} has been successfully deleted.", 
                reservationToDelete.ParkingSpot.Name,
                reservationToDelete.ReservationDate);

            _reservationService.DeleteReservation(reservationToDelete.Id);

            return Json(new { success = true });
        }
    }
}

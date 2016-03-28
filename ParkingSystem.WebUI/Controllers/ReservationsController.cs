using ParkingSystem.Core.Services;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.WebUI.Models;
using System;
using System.Web.Mvc;
using ParkingSystem.Core.Pagination;
using ParkingSystem.WebUI.Identity;
using ParkingSystem.Core.Time;

namespace ParkingSystem.WebUI.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IReservationService _reservationService;
        private readonly ICalendarService _calendarService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly int _pageSize = 8;

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

            var previousWeekOfYear = _calendarService.GetPreviousWeekInYear(currentWeekOfYear);
            var nextWeekOfYear = _calendarService.GetNextWeekInYear(currentWeekOfYear);

            var businessDatesInCurrentWeek = _calendarService.GetDatesOfBusinessDaysInWeek(currentWeekOfYear);

            return View(
                new ReservationsListViewModel
                {
                    BusinessDatesInWeek = businessDatesInCurrentWeek,
                    ParkingSpots = _parkingSpotService.GetAllParkingSpots(),
                    Reservations = _reservationService.GetAllReservationsForDateRange(businessDatesInCurrentWeek.ToList()),
                    PreviousWeekUrl = Url.Action("Calendar", new
                    {
                        year = previousWeekOfYear.Year,
                        week = previousWeekOfYear.Week
                    }),
                    NextWeekUrl = Url.Action("Calendar", new
                    {
                        year = nextWeekOfYear.Year,
                        week = nextWeekOfYear.Week
                    })
                });
        }

        private WeekOfYear GetWeekOfYearToDisplay(int? year, int? week)
        {
            var weekOfYearToDisplay = new WeekOfYear();

            if (year == null && week == null)
            {
                weekOfYearToDisplay = _calendarService.GetCurrentWeekOfYear();
                
                var today = _calendarService.GetTodayDate();

                if (_calendarService.IsWeekendDay(today))
                {
                    weekOfYearToDisplay = _calendarService.GetNextWeekInYear(weekOfYearToDisplay);
                }
            }
            else if (year != null && week != null)
            {
                weekOfYearToDisplay.Year = year.Value;
                weekOfYearToDisplay.Week = week.Value;
            }
            else
            {
                throw new InvalidOperationException();
            }

            return weekOfYearToDisplay;
        }

        public ActionResult UserReservationsTotal()
        {
            var loggedApplicationUser = _applicationUserManager.FindByNameAsync(User.Identity.Name).Result;

            var reservations = _reservationService.GetAllReservationsForUser(
                loggedApplicationUser, _calendarService.GetTodayDate());

            return Content(reservations.Count.ToString());
        }

        public ActionResult UserReservations(int page = 1)
        {
            var loggedApplicationUser = _applicationUserManager.FindByNameAsync(User.Identity.Name).Result;
            var pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = _pageSize };

            var reservations = _reservationService.GetAllReservationsForUser(
                pagingInfo, loggedApplicationUser, _calendarService.GetTodayDate());

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
                    ReservationDate = reservation.ReservationDate,
                    CreatedDate = _calendarService.GetNow()
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

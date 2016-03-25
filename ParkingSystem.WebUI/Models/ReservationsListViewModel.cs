using ParkingSystem.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingSystem.WebUI.Models
{
    public class ReservationsListViewModel
    {
        public IList<DateTime> BusinessDatesInWeek { get; set; }
        public IList<ParkingSpot> ParkingSpots { get; set; }
        public IList<Reservation> Reservations { get; set; }

        public string PreviousWeekUrl { get; set; }
        public string NextWeekUrl { get; set; }

        public Reservation TryGetReservationFor(ParkingSpot parkingSpot, DateTime date)
        {
            return Reservations
                .Where(r => r.ParkingSpot.Id == parkingSpot.Id && r.ReservationDate == date)
                .FirstOrDefault();
        }
    }
}
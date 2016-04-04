using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.WebUI.Models
{
    public class ReservationDetailsViewModel
    {
        public Reservation Reservation { get; set; }
        public bool CanLoggedUserDeleteReservation { get; set; }
    }
}
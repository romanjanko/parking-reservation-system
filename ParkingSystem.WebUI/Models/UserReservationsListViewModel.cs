using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.Pagination;
using System.Linq;

namespace ParkingSystem.WebUI.Models
{
    public class UserReservationsListViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<ReservationAdditionalInfo> ReservationsAdditionalInfo { get; set; }

        public bool CanBeReservationDeletedByUser(int reservationId)
        {
            var reservationAdditionalInfo = ReservationsAdditionalInfo
                .FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservationAdditionalInfo != null)
                return reservationAdditionalInfo.CanBeDeletedByUser;

            return false;
        }
    }

    public class ReservationAdditionalInfo
    {
        public int ReservationId { get; set; }
        public bool CanBeDeletedByUser { get; set; }
    }
}
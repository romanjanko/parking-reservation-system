using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.DomainModel.Models
{
    public class DeletedReservation
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool ReservedFreely { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}

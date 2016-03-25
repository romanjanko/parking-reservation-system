using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.DomainModel.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public Reservation()
        {
            CreatedDate = DateTime.Now;
        }
    }
}

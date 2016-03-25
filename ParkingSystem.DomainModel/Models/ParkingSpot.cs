using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.DomainModel.Models
{
    public enum ParkingSpotType
    {
        Garage,
        Outside
    }

    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Parking spot name is a required field.")]
        [Display(Name = "Parking spot name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Parking spot type is a required field.")]
        [Display(Name = "Parking spot type")]
        public ParkingSpotType Type { get; set; }
    }
}

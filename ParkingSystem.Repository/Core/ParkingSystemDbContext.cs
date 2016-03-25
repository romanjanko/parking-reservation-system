using Microsoft.AspNet.Identity.EntityFramework;
using ParkingSystem.DomainModel.Models;
using System.Data.Entity;

namespace ParkingSystem.Repository.Core
{
    public class ParkingSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public ParkingSystemDbContext() 
            : base("ParkingSystemDbConnectionString")
        {
        }
                
        public static ParkingSystemDbContext Create()
        {
            return new ParkingSystemDbContext();
        }

        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
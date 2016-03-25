namespace ParkingSystem.Repository.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DomainModel.Models;
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ParkingSystem.Repository.Core.ParkingSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParkingSystem.Repository.Core.ParkingSystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var passwordHasher = new PasswordHasher();

            // admin user and its role
            var adminsRole = new IdentityRole { Name = "Admins" };
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                PasswordHash = passwordHasher.HashPassword(ConfigurationManager.AppSettings["adminPassword"]),
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = "admin",
                EmailConfirmed = true
            };

            context.Roles.AddOrUpdate(r => r.Name, adminsRole);
            context.Users.AddOrUpdate(u => u.UserName, adminUser);

            // add "admin" user to "Admins" roles
            context.SaveChanges(); //TODO
            adminsRole = context.Roles.FirstOrDefault(r => r.Name == adminsRole.Name);
            adminUser = context.Users.FirstOrDefault(u => u.UserName == adminUser.UserName);

            if (adminsRole != null && adminUser != null)
            {
                if (adminsRole.Users.FirstOrDefault(ur => ur.RoleId == adminsRole.Id && ur.UserId == adminUser.Id) == null)
                    adminsRole.Users.Add(new IdentityUserRole { RoleId = adminsRole.Id, UserId = adminUser.Id });
            }

            // parking spots
            var ps12 = new ParkingSpot { Name = "12", Type = ParkingSpotType.Garage };
            var ps13 = new ParkingSpot { Name = "13", Type = ParkingSpotType.Garage };
            var ps15 = new ParkingSpot { Name = "15", Type = ParkingSpotType.Garage };
            var ps16 = new ParkingSpot { Name = "16", Type = ParkingSpotType.Garage };
            var ps17 = new ParkingSpot { Name = "17", Type = ParkingSpotType.Garage };

            var ps126 = new ParkingSpot { Name = "126", Type = ParkingSpotType.Outside };
            var ps127 = new ParkingSpot { Name = "127", Type = ParkingSpotType.Outside };
            var ps128 = new ParkingSpot { Name = "128", Type = ParkingSpotType.Outside };
            var ps129 = new ParkingSpot { Name = "129", Type = ParkingSpotType.Outside };
            var ps130 = new ParkingSpot { Name = "130", Type = ParkingSpotType.Outside };

            context.ParkingSpots.AddOrUpdate(ps => ps.Name,
                ps12, ps13, ps15, ps16, ps17, ps126, ps127, ps128, ps129, ps130);
        }
    }
}

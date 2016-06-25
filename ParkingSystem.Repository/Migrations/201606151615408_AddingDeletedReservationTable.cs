namespace ParkingSystem.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeletedReservationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeletedReservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        ReservedFreely = c.Boolean(nullable: false),
                        DeletedDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ParkingSpot_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ParkingSpots", t => t.ParkingSpot_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ParkingSpot_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeletedReservations", "ParkingSpot_Id", "dbo.ParkingSpots");
            DropForeignKey("dbo.DeletedReservations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DeletedReservations", new[] { "ParkingSpot_Id" });
            DropIndex("dbo.DeletedReservations", new[] { "ApplicationUser_Id" });
            DropTable("dbo.DeletedReservations");
        }
    }
}

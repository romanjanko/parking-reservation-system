namespace ParkingSystem.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FreeReservations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "ReservedFreely", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "ReservedFreely");
        }
    }
}

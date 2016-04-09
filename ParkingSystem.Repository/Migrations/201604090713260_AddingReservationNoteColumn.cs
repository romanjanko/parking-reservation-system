namespace ParkingSystem.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingReservationNoteColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "ReservationNote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "ReservationNote");
        }
    }
}

namespace ParkingSystem.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAcountActiveField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserAccountActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserAccountActive");
        }
    }
}

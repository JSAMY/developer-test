namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Appointments",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),                  
                   BuyerUserId = c.String(nullable: false, maxLength: 128),
                   Property_Id = c.Int(),
                   AppointmentDateTime = c.DateTime(nullable: false),
                   Status = c.Int(nullable: false),
                   CreatedAt = c.DateTime(nullable:false)
               })
               .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.Property_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerUserId)
                .Index(t => t.Property_Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}

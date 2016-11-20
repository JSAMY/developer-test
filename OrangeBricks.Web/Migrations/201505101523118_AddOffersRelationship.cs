namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOffersRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Property_Id = c.Int(),
                        BuyerUserId = c.String(nullable: false, maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.Property_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerUserId)
                .Index(t => t.Property_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "Property_Id", "dbo.Properties");
            DropIndex("dbo.Offers", new[] { "Property_Id" });
            DropTable("dbo.Offers");
        }
    }
}

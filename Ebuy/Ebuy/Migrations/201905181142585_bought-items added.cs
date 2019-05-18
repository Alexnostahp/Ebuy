namespace Ebuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boughtitemsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoughtItems",
                c => new
                    {
                        BoughtItemId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ItemId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BoughtItemId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoughtItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BoughtItems", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BoughtItems", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.BoughtItems", new[] { "ItemId" });
            DropTable("dbo.BoughtItems");
        }
    }
}

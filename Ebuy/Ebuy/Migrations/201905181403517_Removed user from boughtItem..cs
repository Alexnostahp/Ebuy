namespace Ebuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveduserfromboughtItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BoughtItems", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BoughtItems", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.BoughtItems", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BoughtItems", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BoughtItems", "ApplicationUser_Id");
            AddForeignKey("dbo.BoughtItems", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

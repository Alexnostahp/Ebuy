namespace Ebuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            AddColumn("dbo.Items", "Cart_CartId", c => c.Int());
            CreateIndex("dbo.Items", "Cart_CartId");
            AddForeignKey("dbo.Items", "Cart_CartId", "dbo.Carts", "CartId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.Items", new[] { "Cart_CartId" });
            DropColumn("dbo.Items", "Cart_CartId");
            DropTable("dbo.Carts");
        }
    }
}

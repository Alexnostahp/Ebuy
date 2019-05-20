namespace Ebuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartSingeItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.Items", new[] { "Cart_CartId" });
            CreateIndex("dbo.Carts", "ItemId");
            AddForeignKey("dbo.Carts", "ItemId", "dbo.Items", "ItemId", cascadeDelete: true);
            DropColumn("dbo.Items", "Cart_CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Cart_CartId", c => c.Int());
            DropForeignKey("dbo.Carts", "ItemId", "dbo.Items");
            DropIndex("dbo.Carts", new[] { "ItemId" });
            CreateIndex("dbo.Items", "Cart_CartId");
            AddForeignKey("dbo.Items", "Cart_CartId", "dbo.Carts", "CartId");
        }
    }
}

namespace Ebuy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useIdincartisnowstring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carts", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "UserId", c => c.Int(nullable: false));
        }
    }
}

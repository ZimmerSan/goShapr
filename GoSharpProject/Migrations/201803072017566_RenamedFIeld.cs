namespace GoSharpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedFIeld : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderStartus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderStartus", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}

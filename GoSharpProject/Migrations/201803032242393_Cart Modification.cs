namespace GoSharpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartModification : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Carts", newName: "CartRecords");
            DropColumn("dbo.CartRecords", "ProductItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartRecords", "ProductItemId", c => c.Int(nullable: false));
            RenameTable(name: "dbo.CartRecords", newName: "Carts");
        }
    }
}

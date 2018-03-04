namespace GoSharpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SiteTemplateMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductItems", newName: "SiteTemplates");
            DropForeignKey("dbo.Carts", "ProductItemId", "dbo.ProductItems");
            DropIndex("dbo.Carts", new[] { "ProductItemId" });
            AddColumn("dbo.Carts", "SiteTemplate_Id", c => c.Int());
            AddColumn("dbo.SiteTemplates", "Category", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "SiteTemplate_Id");
            AddForeignKey("dbo.Carts", "SiteTemplate_Id", "dbo.SiteTemplates", "Id");
            DropColumn("dbo.SiteTemplates", "Categorie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteTemplates", "Categorie", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "SiteTemplate_Id", "dbo.SiteTemplates");
            DropIndex("dbo.Carts", new[] { "SiteTemplate_Id" });
            DropColumn("dbo.SiteTemplates", "Category");
            DropColumn("dbo.Carts", "SiteTemplate_Id");
            CreateIndex("dbo.Carts", "ProductItemId");
            AddForeignKey("dbo.Carts", "ProductItemId", "dbo.ProductItems", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.SiteTemplates", newName: "ProductItems");
        }
    }
}

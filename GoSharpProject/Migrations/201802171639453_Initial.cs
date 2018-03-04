namespace GoSharpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductItemId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.ProductItems", t => t.ProductItemId, cascadeDelete: true)
                .Index(t => t.ProductItemId);
            
            CreateTable(
                "dbo.ProductItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 160),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InternalImage = c.Binary(),
                        ItemPictureUrl = c.String(maxLength: 1024),
                        Categorie = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.ProductItems", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        OrderStartus = c.Int(nullable: false),
                        DetailDescription = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaveInfo = c.Boolean(nullable: false),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        RoleName = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Salary = c.Double(),
                        StartWorkDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Role_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.Role_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.Role_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Costs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProjectStatus = c.Int(nullable: false),
                        NameProjectManager = c.String(),
                        Order_Id = c.Int(),
                        ProjectManager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProjectManager_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.ProjectManager_Id);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        assignedProject_Id = c.Int(),
                        AssignedWorker_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.assignedProject_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedWorker_Id)
                .Index(t => t.assignedProject_Id)
                .Index(t => t.AssignedWorker_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProjectTasks", "AssignedWorker_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectTasks", "assignedProject_Id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProjectManager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Carts", "ProductItemId", "dbo.ProductItems");
            DropForeignKey("dbo.ProductItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.AspNetUserRoles", "Role_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderDetails", "ItemId", "dbo.ProductItems");
            DropIndex("dbo.ProjectTasks", new[] { "AssignedWorker_Id" });
            DropIndex("dbo.ProjectTasks", new[] { "assignedProject_Id" });
            DropIndex("dbo.Projects", new[] { "ProjectManager_Id" });
            DropIndex("dbo.Projects", new[] { "Order_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Role_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ItemId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.ProductItems", new[] { "Order_Id" });
            DropIndex("dbo.Carts", new[] { "ProductItemId" });
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.Projects");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.ProductItems");
            DropTable("dbo.Carts");
        }
    }
}

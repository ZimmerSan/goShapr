using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GoSharpProject.Models;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            context.CreateRole(roleManager, RolesConst.ADMIN);
            context.CreateRole(roleManager, RolesConst.CUSTOMER);
            context.CreateRole(roleManager, RolesConst.DEVELOPER);
            context.CreateRole(roleManager, RolesConst.MANAGER);
            // Create my debug (testing) objects here

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var admin = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                RoleName = RolesConst.ADMIN,
                Name = RolesConst.ADMIN
            };
            if (userManager.Create(admin, "Pas@123").Succeeded)
                context.AddUserToRole(userManager, admin.Id, RolesConst.ADMIN);

            /*
            ApplicationUser customer = new Customer();
            customer.UserName = "okpr@gmail.com";
            customer.RoleName = RolesConst.CUSTOMER;
            customer.Email = "okpr@gmail.com";
            customer.Name = RolesConst.CUSTOMER;
            if (userManager.Create(customer, "Pas@123").Succeeded)
                context.AddUserToRole(userManager, customer.Id, RolesConst.CUSTOMER);

            Customer customer2 = new Customer();
            customer2.UserName = "customer2@gmail.com";
            customer2.Email = "customer2@gmail.com";
            customer2.RoleName = RolesConst.CUSTOMER;
            customer2.Name = RolesConst.CUSTOMER;
            if (userManager.Create(customer2, "Pas@123").Succeeded)
                context.AddUserToRole(userManager, customer2.Id, RolesConst.CUSTOMER);

            ApplicationUser dev1 = new ApplicationUser();
            dev1.UserName = "dev@gmail.com";
            dev1.Email = "dev@gmail.com";
            dev1.RoleName = RolesConst.DEVELOPER;
            dev1.Name = RolesConst.DEVELOPER;
            if (userManager.Create(dev1, "Pas@123").Succeeded)
                context.AddUserToRole(userManager, dev1.Id, RolesConst.DEVELOPER);

            ApplicationUser manager = new ApplicationUser();
            manager.UserName = "manager@gmail.com";
            manager.Email = "manager@gmail.com";
            manager.RoleName = RolesConst.MANAGER;
            manager.Name = RolesConst.MANAGER;
            if (userManager.Create(manager, "Pas@123").Succeeded)
                context.AddUserToRole(userManager, manager.Id, RolesConst.MANAGER);

            ApplicationUser manager2 = new ApplicationUser();
            manager2.UserName = "manager2@gmail.com";
            manager2.Email = "manager2@gmail.com";
            manager2.RoleName = RolesConst.MANAGER;
            manager2.Name = RolesConst.MANAGER;
            if (userManager.Create(manager2, "Pas@123").Succeeded)
                context.AddUserToRole(userManager, manager2.Id, RolesConst.MANAGER);

            context.SaveChanges();

            SiteTemplate item1 = new SiteTemplate
            {
                Name = "Market Template",
                Price = 100,
                ShortDescription = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium",
                Description = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?",
                Category = TemplateSiteTypes.Shop
            };
            SiteTemplate item2 = new SiteTemplate
            {
                Name = "Blog Template",
                Price = 234,
                ShortDescription = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit",
                Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.",
                Category = TemplateSiteTypes.Blog
            };
            context.ProductItems.Add(item1);
            context.ProductItems.Add(item2);

            Order or1 = new Order
            {
                DueDate = DateTime.Now,
                OrderDate = DateTime.Now,
                DetailDescription = "mememe",
                OrderStartus = OrderStatus.Initiating,
                Total = 200,
                Customer = new Customer
                {
                    Email = "@",
                    Name = "A",
                    RoleName = RolesConst.CUSTOMER,
                    UserName = "Nam"
                }

            };


            context.Orders.Add(or1);

            Order or2 = new Order
            {
                DueDate = DateTime.Now,
                OrderDate = DateTime.Now,
                DetailDescription = "nyanyayna",
                OrderStartus = OrderStatus.Processiong,
                Total = 150


            };

            Order or3 = new Order
            {
                DueDate = DateTime.Now,
                OrderDate = DateTime.Now,
                DetailDescription = "sysysy",
                OrderStartus = OrderStatus.Initiating,
                Total = 600


            };

            context.Orders.Add(or2);
            context.Orders.Add(or3);

            Project project = new Project
            {
                Name = "MyProject",
                NameProjectManager = "manager@gmail.com",
                Costs = 300,
                ProjectStatus = ProjectStatus.Initial,
                ProjectManager = manager,
                Order = or1,
            };
            context.Projects.Add(project);
            */
            context.SaveChanges();
        }
    }
}

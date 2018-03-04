using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is NULL");
            }

            base.OnModelCreating(modelBuilder);

            //Defining the keys and relations
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationRole>().HasKey(r => r.Id).ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.UserRoles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new {r.UserId, r.RoleId }).ToTable("AspNetUserRoles");
        }

        public bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        public bool CreateRole(ApplicationRoleManager roleManager, string name)
        {
            var idResult = roleManager.Create(new ApplicationRole(name));
            return idResult.Succeeded;
        }

        public bool AddUserToRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            var idResult = userManager.AddToRole(userId, roleName);
       
            return idResult.Succeeded;
        }

        public void ClearUserRoles(ApplicationUserManager userManager, string userId)
        {
            var user = userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.UserRoles);
            foreach (var identityUserRole in currentRoles)
            {
                var role = (ApplicationUserRole) identityUserRole;
                userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }

        public void RemoveFromRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            userManager.RemoveFromRole(userId, roleName);
        }

        public void DeleteRole(ApplicationDbContext context, ApplicationUserManager userManager, string roleId)
        {
            var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.RoleId == roleId));
            var role = context.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                if (role != null) RemoveFromRole(userManager, user.Id, role.Name);
            }

            if (role != null) context.Roles.Remove(role);
            context.SaveChanges();
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SiteTemplate> ProductItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<CartRecord> Carts { get; set; }
    }   
}
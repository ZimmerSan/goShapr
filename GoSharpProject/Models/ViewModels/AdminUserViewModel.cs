
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.ViewModels
{
    public class AdminUserViewModel
    {
        public string UserName { get; set; }
         [Required]
        public string Email { get; set; }
         [Required]
        public string Name { get; set; }
        public string Organization { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string Id { get; set; }


        public AdminUserViewModel() { }
        public AdminUserViewModel(ApplicationUser user)
        {

            this.UserName = user.UserName;
            this.Email = user.Email;
            this.Name = user.Name;
            this.Role = user.RoleName;
            this.Password = user.PasswordHash;
            this.RoleName = user.RoleName;
            this.Id = user.Id;
        }
    }
}
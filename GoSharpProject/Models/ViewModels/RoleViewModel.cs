using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.ViewModels
{
    public class RoleViewModel
    {
        public string RoleName { get; set; }

        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            this.RoleName = role.Name;
        }
    }

    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }

        // Update this to accept an argument of type ApplicationRole:
        public SelectRoleEditorViewModel(ApplicationRole role)
        {
            this.RoleName = role.Name;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }
    }

    public class EditRoleViewModel
    {
        public string OriginalRoleName { get; set; }
        public string RoleName { get; set; }

        public EditRoleViewModel() { }
        public EditRoleViewModel(ApplicationRole role)
        {
            this.OriginalRoleName = role.Name;
            this.RoleName = role.Name;
        }
    }
}
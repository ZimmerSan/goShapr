using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSharpProject.Models.entities
{
    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationRole Role { get; set; }
    }
   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSharpProject.Models.entities
{
    public class Customer : ApplicationUser
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}
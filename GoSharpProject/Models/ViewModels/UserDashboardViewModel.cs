using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.ViewModels
{
    public class UserDashboardViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Order> OrdersToBeApproved { get; set; }
    }
}
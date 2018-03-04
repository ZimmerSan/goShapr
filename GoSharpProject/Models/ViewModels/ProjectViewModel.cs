using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public decimal costs { get; set; }
        public ProjectStatus projectStatus { get; set; }
        public virtual ApplicationUser projectManager { get; set; }
        public String nameProjectManager { get; set; }
        public virtual Order order { get; set; }

        public ProjectViewModel() { }
        public ProjectViewModel(Project item)
        {
            this.id = item.Id;
            this.name = item.Name;
            this.costs = item.Costs;
            this.projectStatus = item.ProjectStatus;
            this.projectManager = item.ProjectManager;
            this.nameProjectManager = item.NameProjectManager;
            this.order = item.Order;
        }
    }
}
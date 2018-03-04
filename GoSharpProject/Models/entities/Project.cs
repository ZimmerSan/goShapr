using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoSharpProject.Models.constants;

namespace GoSharpProject.Models.entities
{
    public class Project
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        public decimal Costs { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public virtual ApplicationUser ProjectManager { get; set; }

        public string NameProjectManager { get; set; }
        public virtual Order Order {get;set; }
    }
}
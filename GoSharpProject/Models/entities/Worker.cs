using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace GoSharpProject.Models.entities
{
    public class Worker : ApplicationUser
    {
        public double Salary { get; set; }
        public DateTime StartWorkDate { get; set; }
    }
}
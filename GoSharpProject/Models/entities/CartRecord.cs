using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoSharpProject.Models.entities
{

    public class CartRecord
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }

        public DateTime DateCreated { get; set; }
        public virtual SiteTemplate SiteTemplate { get; set; }
    }

}
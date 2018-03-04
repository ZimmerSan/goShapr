using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSharpProject.Models.entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual SiteTemplate Item { get; set; }
        public virtual Order Order { get; set; }

        public override string ToString()
        {
            return Item.Name+" " + Quantity + " * "+ Item.Price;
        }
    }
}
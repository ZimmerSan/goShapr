using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
        public List<CartRecord> CartRecords { get; set; }
        public int ItemsCount { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ShoppingCartSummaryViewModel
    {
        public int ItemsCount { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}
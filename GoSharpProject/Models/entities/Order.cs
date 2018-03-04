using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models.constants;

namespace GoSharpProject.Models.entities
{
    [Bind(Exclude = "Id")]
    public class Order
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public OrderStatus OrderStartus { get; set; }
        public virtual ICollection<SiteTemplate> OrderItems { get; set; }
        public string DetailDescription { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Customer Customer { get; set; }
        public bool SaveInfo { get; set; }

        public string ToString(Order order)
        {
            StringBuilder bob = new StringBuilder();

            bob.Append("<p>Order Information for Order: " + order.Id + "<br>Placed at: " + order.OrderDate + "</p>").AppendLine();
            bob.Append("<p>Name: " + order.Customer.Name + "<br>");
            bob.Append("<br>").AppendLine();
            bob.Append("<Table>").AppendLine();
            // Display header 
            string header = "<tr> <th>Item Name</th>" + "<th>Quantity</th>" + "<th>Price</th> <th></th> </tr>";
            bob.Append(header).AppendLine();

            String output = String.Empty;
            try
            {
                foreach (var item in order.OrderDetails)
                {
                    bob.Append("<tr>");
                    output = "<td>" + item .Item.Name+ "</td>" + "<td>" + item.Quantity + "</td>" + "<td>" + item.Quantity * item.UnitPrice + "</td>";
                    bob.Append(output).AppendLine();
                    Console.WriteLine(output);
                    bob.Append("</tr>");
                }
            }
            catch (Exception ex)
            {
                output = "No items ordered.";
            }
            bob.Append("</Table>");
            bob.Append("<b>");
            // Display footer 
            string footer = String.Format("{0,-12}{1,12}\n",
                                          "Total", order.Total);
            bob.Append(footer).AppendLine();
            bob.Append("</b>");

            return bob.ToString();
        }
    }
  
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.repository;

namespace GoSharpProject.Controllers
{
    public class CheckoutController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Customer")]
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            //try
            //{
               /* if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {*/
                   // order.customer.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    order.DueDate = DateTime.Now;
                    order.OrderStartus = OrderStatus.Initiating;
                    order.DetailDescription = values[0];
                   
                    order.Customer =(Customer) unitOfWork.CustomerRepository.dbSet.Where(s => s.UserName.Equals(User.Identity.Name)).First();
                  

                    //Save Order
                    unitOfWork.OrderRepository.Insert(order);
                    unitOfWork.Save();
                    //Process the order
                    var cart = OrderCart.GetCart(this);
                 //   order.orderItems = new Collection<SiteTemplate>();
                    cart.CreateOrder(order);
                    unitOfWork.Save();

                    return RedirectToAction("Complete",
                        new { id = order.Id });
                   
                //}
           // }
            //catch
            //{
                //Invalid - redisplay with errors
            //    return View(order);
           // }
        }
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = unitOfWork.OrderRepository.dbSet.Any(
                o => o.Id == id &&
                o.Customer.UserName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
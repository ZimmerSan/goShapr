using GoSharpProject.Models;
using GoSharpProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.repository;

namespace GoSharpProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            int count = cart.AddToCart(id);

            // Retrieve the item from the database
            var addedItem = unitOfWork.SiteTemplateRepository.dbSet
                .Single(item => item.Id == id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedItem.Name) + " has been added to your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = count,
                DeleteId = id
            };
            return Json(results);

            // Go back to the main store page for more shopping
            // return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string itemName = unitOfWork.SiteTemplateRepository.dbSet
                .Single(item => item.Id == id).Name;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = "One (1) " + Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

        [HttpPost]
        [Authorize(Roles = RolesConst.CUSTOMER)]
        public ActionResult Checkout()
        {
            var order = new Order();
            TryUpdateModel(order);

            order.OrderDate = DateTime.Now;
            order.DueDate = DateTime.Now;
            order.OrderStatus = OrderStatus.Initiating;
            order.Customer = (Customer)unitOfWork.CustomerRepository.dbSet.Where(s => s.UserName.Equals(User.Identity.Name)).First();

            //Save Order
            unitOfWork.OrderRepository.Insert(order);
            unitOfWork.Save();

            //Process the order
            var cart = OrderCart.GetCart(this);
            //   order.orderItems = new Collection<SiteTemplate>();
            cart.CreateOrder(order);
            unitOfWork.Save();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}
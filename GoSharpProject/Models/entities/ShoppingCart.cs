using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models.repository;

namespace GoSharpProject.Models.entities
{
    public partial class ShoppingCart
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(int templateId)
        {
            
            // Get the matching cart and item instances
            var cartRecord = _unitOfWork.CartRepository.dbSet.SingleOrDefault(c => c.CartId == ShoppingCartId && c.SiteTemplate != null && c.SiteTemplate.Id == templateId);

            if (cartRecord == null)
            {
                cartRecord = new CartRecord
                {
                    SiteTemplate = _unitOfWork.SiteTemplateRepository.dbSet.Find(templateId),
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                cartRecord = _unitOfWork.CartRepository.dbSet.Add(cartRecord);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartRecord.Count++;
            }
            // Save changes
            _unitOfWork.Save();
           // storeDB.SaveChanges();

            return cartRecord.Count;
        }

        public int RemoveFromCart(int id)
        {
            var cartRecord = _unitOfWork.CartRepository.dbSet.Single(cart => cart.CartId == ShoppingCartId && cart.SiteTemplate != null && cart.SiteTemplate.Id.Equals(id));
            int itemCount = 0;

            if (cartRecord != null)
            {
                if (cartRecord.Count > 1)
                {
                    cartRecord.Count--;
                    itemCount = cartRecord.Count;
                }
                else
                {
                    _unitOfWork.CartRepository.Delete(cartRecord);
                }
                _unitOfWork.Save();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _unitOfWork.CartRepository.dbSet.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _unitOfWork.CartRepository.Delete(cartItem);
            }
            // Save changes
            _unitOfWork.Save();
            //storeDB.SaveChanges();
        }

        public List<CartRecord> GetCartRecords()
        {
            return _unitOfWork.CartRepository.dbSet.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _unitOfWork.CartRepository.dbSet
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply item price by count of that item to get 
            // the current price for each of those items in the cart
            // sum all item price totals to get the cart total
            decimal? total = (from cartItems in _unitOfWork.CartRepository.dbSet
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.SiteTemplate.Price).Sum();

            return total ?? decimal.Zero;
        }

        public Order CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            order.OrderDetails = new List<OrderDetail>();

            var cartItems = GetCartRecords();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemId = item.SiteTemplate.Id,
                    OrderId = order.Id,
                    UnitPrice = item.SiteTemplate.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.SiteTemplate.Price);
                order.OrderDetails.Add(orderDetail);
                _unitOfWork.OrderDetailRepository.Insert(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            _unitOfWork.Save();
            //storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the Id as the confirmation number
            return order;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _unitOfWork.CartRepository.dbSet.Where(
                c => c.CartId == ShoppingCartId);

            foreach (CartRecord item in shoppingCart)
            {
                item.CartId = userName;
            }
            _unitOfWork.Save();
            //storeDB.SaveChanges();
        }
    }
}
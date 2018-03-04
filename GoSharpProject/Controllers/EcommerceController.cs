using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.repository;
using GoSharpProject.Models.ViewModels;
using PagedList;

namespace GoSharpProject.Controllers
{
    public class EcommerceController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult ProductsGrid(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            } else {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in unitOfWork.SiteTemplateRepository.dbSet select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                         || s.Category.ToString().ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;
                default:  // Name ascending 
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProductsList()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        

        public ActionResult Payments()
        {
            return View();
        }

        public ActionResult Cart()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartRecords = cart.GetCartRecords(),
                ItemsCount = cart.GetCount(),
                TotalPrice = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        // -- -- //

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SiteTemplate item = await unitOfWork.SiteTemplateRepository.dbSet.FindAsync(id);

            if (item == null) return HttpNotFound();

            return View(item);
        }

        [Authorize(Roles = RolesConst.ADMIN + ", " + RolesConst.MANAGER)]
        public ActionResult Create()
        {
            return View(new SiteTemplate());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConst.ADMIN + ", " + RolesConst.MANAGER)]
        public async Task<ActionResult> Create(SiteTemplate item)
        {
            if (ModelState.IsValid)
            {
                item = unitOfWork.SiteTemplateRepository.context.ProductItems.Add(item);
                await unitOfWork.SiteTemplateRepository.context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = item.Id });
            }
            return View(item);
        }

        [Authorize(Roles = RolesConst.ADMIN + ", " + RolesConst.MANAGER)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SiteTemplate item = await unitOfWork.SiteTemplateRepository.dbSet.FindAsync(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConst.ADMIN + ", " + RolesConst.MANAGER)]
        public async Task<ActionResult> Edit(SiteTemplate item)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SiteTemplateRepository.context.Entry(item).State = EntityState.Modified;
                await unitOfWork.SiteTemplateRepository.context.SaveChangesAsync();
                return RedirectToAction("Details", new{ id = item.Id });
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConst.ADMIN + ", " + RolesConst.MANAGER)]
        public async Task<ActionResult> Delete(int id)
        {
            SiteTemplate item = await unitOfWork.SiteTemplateRepository.context.ProductItems.FindAsync(id);
            unitOfWork.SiteTemplateRepository.context.ProductItems.Remove(item);
            await unitOfWork.SiteTemplateRepository.context.SaveChangesAsync();
            return RedirectToAction("ProductsGrid");
        }

        [HttpGet]
        public JsonResult GetItem(int id)
        {
            var foundItem = unitOfWork.SiteTemplateRepository.dbSet.Single(item => item.Id == id);
            return Json(foundItem, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            return Json(new ShoppingCartSummaryViewModel
            {
                ItemsCount = cart.GetCount(),
                TotalPrice = cart.GetTotal()
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            SiteTemplate item = await unitOfWork.SiteTemplateRepository.context.ProductItems.FindAsync(id);
            return File(item.InternalImage, "image/png");
        }
    }
}
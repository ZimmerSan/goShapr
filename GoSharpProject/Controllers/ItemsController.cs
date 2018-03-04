using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using GoSharpProject.Models;
using GoSharpProject.Models.repository;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.constants;

namespace GoSharpProject.Controllers
{
    public class ItemsController : Controller
    {
        private UnitOfWork unityOfWork = new UnitOfWork();
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in unityOfWork.SiteTemplateRepository.dbSet
                           select i;
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

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteTemplate item = await unityOfWork.SiteTemplateRepository.dbSet.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

       
        // GET: Items/Create
        [Authorize(Roles = "SalesManager")]
        public ActionResult Create()
        {
           // ViewBag.CatagorieId = new SelectList(TemplateSiteTypes);
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesManager")]
        public async Task<ActionResult> Create(SiteTemplate item)
        {
            if (ModelState.IsValid)
            {
                unityOfWork.SiteTemplateRepository.context.ProductItems.Add(item);
               // db.Items.Add(item);
                await unityOfWork.SiteTemplateRepository.context.SaveChangesAsync();
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

           // ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name", item.CatagorieId);
            return View(item);
        }

        // GET: Items/Edit/5
         [Authorize(Roles = "SalesManager")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteTemplate item = await unityOfWork.SiteTemplateRepository.context.ProductItems.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
           // ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name", item.CatagorieId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesManager")]
        public async Task<ActionResult> Edit(SiteTemplate item)
        {
            if (ModelState.IsValid)
            {
                unityOfWork.SiteTemplateRepository.context.Entry(item).State = EntityState.Modified;
                await unityOfWork.SiteTemplateRepository.context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.CatagorieId = new SelectList(db.Catagories, "ID", "Name", item.CatagorieId);
            return View(item);
        }

        // GET: Items/Delete/5
         [Authorize(Roles = "SalesManager")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteTemplate item = await unityOfWork.SiteTemplateRepository.context.ProductItems.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesManager")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SiteTemplate item = await unityOfWork.SiteTemplateRepository.context.ProductItems.FindAsync(id);
            unityOfWork.SiteTemplateRepository.context.ProductItems.Remove(item);
            await unityOfWork.SiteTemplateRepository.context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            SiteTemplate item = await unityOfWork.SiteTemplateRepository.context.ProductItems.FindAsync(id);

            byte[] photoBack = item.InternalImage;

            return File(photoBack, "image/png");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unityOfWork.SiteTemplateRepository.context.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }
}

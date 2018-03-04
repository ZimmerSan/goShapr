using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using GoSharpProject.Models;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.repository;

namespace GoSharpProject.Controllers
{
    public class SalesManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: SalesManager
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var productItemList = new Collection<SiteTemplate>();
            var productI = from s in _db.ProductItems
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                productI = productI.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    productI = productI.OrderByDescending(s => s.Name);
                    break;
                default:
                    productI = productI.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(productI.ToPagedList(pageNumber, pageSize));
            
            //return View(unitOfWork.SiteTemplateRepository.Get().ToList());
        }

        // GET: SalesManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteTemplate siteTemplate = unitOfWork.SiteTemplateRepository.GetByID(id);
            if (siteTemplate == null)
            {
                return HttpNotFound();
            }
            return View(siteTemplate);
        }

        // GET: SalesManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,shortDescription,description,price")] SiteTemplate siteTemplate)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SiteTemplateRepository.Insert(siteTemplate);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(siteTemplate);
        }

        // GET: SalesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteTemplate siteTemplate = unitOfWork.SiteTemplateRepository.GetByID(id);
            if (siteTemplate == null)
            {
                return HttpNotFound();
            }
            return View(siteTemplate);
        }

        // POST: SalesManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SiteTemplate siteTemplate)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SiteTemplateRepository.Update(siteTemplate);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(siteTemplate);
        }

        // GET: SalesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteTemplate siteTemplate = unitOfWork.SiteTemplateRepository.GetByID(id);
            if (siteTemplate == null)
            {
                return HttpNotFound();
            }
            return View(siteTemplate);
        }

        // POST: SalesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteTemplate siteTemplate = unitOfWork.SiteTemplateRepository.GetByID(id);
            unitOfWork.SiteTemplateRepository.Delete(siteTemplate);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }



       
    }
}

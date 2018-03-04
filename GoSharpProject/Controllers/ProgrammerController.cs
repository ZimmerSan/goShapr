using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PagedList;
using GoSharpProject.Models.entities;
using GoSharpProject.Models;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.repository;
using GoSharpProject.Models.ViewModels;

namespace GoSharpProject.Controllers
{
    public class ProgrammerController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private ApplicationDbContext _db = new ApplicationDbContext();

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
            new Collection<ProjectTask>();
            var workI1 = from s in _db.ProjectTasks select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                workI1 = workI1.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    workI1 = workI1.OrderByDescending(s => s.Name);
                    break;
                default:
                    workI1 = workI1.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(workI1.ToPagedList(pageNumber, pageSize));

            //return View(unitOfWork.SiteTemplateRepository.Get().ToList());
        } 

     public ActionResult Details(int id)
        {
           if (id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = unitOfWork.WorkItemRepository.GetByID(id);
            if (projectTask == null)
            {
                  return HttpNotFound();
            }
            return View(projectTask);
        }

        // GET: /Worker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Worker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Organization,City,Country,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Salary,startWorkDate")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                //db.Users.Add(worker);
              //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(worker);
        }

        // GET: /Worker/Edit/5
        public ActionResult Edit(int  id)
        {
            ProjectTask workI = unitOfWork.WorkItemRepository.GetByID(id);
            
            ViewBag.ps = (IEnumerable<TaskStatus>)Enum.GetValues(typeof(TaskStatus));
            if (workI == null)
            {
               return HttpNotFound();
            }
            return View(workI);
            //return View(new WorkItemViewModel(workI));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectTask model)
        {
            if (ModelState.IsValid)
            {
                var workI = unitOfWork.WorkItemRepository.GetByID(model.Id);
                 workI.Status= model.Status;
                 unitOfWork.WorkItemRepository.Update(workI);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
              //  db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

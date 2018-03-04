using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.repository;
using GoSharpProject.Models.ViewModels;

namespace GoSharpProject.Controllers
{
    public class OperatorController : Controller
    {
        
        UnitOfWork unitOfWork = new UnitOfWork();

         [Authorize(Roles = "Operator")] 
        // GET: Operator
        public ActionResult Index()
        {
            IEnumerable<ProjectTask> workItems = unitOfWork.WorkItemRepository.Get().ToList();
            return View(workItems);
        }

        // GET: Operator/Details/5

        public ActionResult Details(string id = "")
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
            return View();
        }

        // GET: Operator/Create
        public ActionResult Create()
        {
            ViewBag.workItems = unitOfWork.WorkItemRepository.Get().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkItemViewModel model)
        {
                var @workItem = new ProjectTask() {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    AssignedWorker = model.AssignedWorker,
                    assignedProject = model.AssignedProject
                };

                unitOfWork.WorkItemRepository.Insert(@workItem);
                unitOfWork.Save();

                return RedirectToAction("Index", "Operator");
        }

        // GET: Operator/Edit/5
        public ActionResult Edit(string id = "")
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkItemViewModel model)
        {
            ProjectTask projectTask = unitOfWork.WorkItemRepository.GetByID(model.Id);
            projectTask.Name = model.Name;
            projectTask.Description = model.Description;
            projectTask.DueDate = model.DueDate;
            projectTask.Status = model.Status;
            projectTask.AssignedWorker = model.AssignedWorker;
            projectTask.assignedProject = model.AssignedProject;

            unitOfWork.WorkItemRepository.Insert(projectTask);
            unitOfWork.Save();
            
            return RedirectToAction("Index");
        }

        // GET: Operator/Delete/5
        public ActionResult Delete(string id = "")
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

        // POST: Operator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id = "")
        {
            ProjectTask projectTask = unitOfWork.WorkItemRepository.GetByID(id);
            unitOfWork.WorkItemRepository.Delete(projectTask);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    
    }
}

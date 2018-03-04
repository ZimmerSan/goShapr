using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models.entities;
using GoSharpProject.Models;
using GoSharpProject.Models.repository;
using GoSharpProject.Models.constants;

namespace GoSharpProject.Controllers
{
    public class OrderOperatorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private IEnumerable<Order> activeOrders;

        // GET: /OrderOperator/
        public ActionResult Index()
        {
            activeOrders  = unitOfWork.OrderRepository.Get().Where(s => s.OrderStartus.Equals(OrderStatus.Initiating));
            return View(activeOrders);
        }



        public ActionResult Reject(int? id)
        {
            Order ord = unitOfWork.OrderRepository.GetByID(id);
            ord.OrderStartus = OrderStatus.Rejected;
            unitOfWork.OrderRepository.Update(ord);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


        public ActionResult Confrim(int? id)
        {

            IEnumerable<ApplicationUser> them = unitOfWork.UserRepository.Get().Where(s => s.RoleName.Equals(RolesConst.MANAGER));

            ViewBag.pm = them;
            //make enumerable from enum
            //ViewBag.ps = (IEnumerable<ProjectStatus>)Enum.GetValues(typeof(ProjectStatus)); 
            Project proj = new Project();
           
            return View(proj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confrim(Project pro)
        {
            if (ModelState.IsValid)
            {
                Order ord = unitOfWork.OrderRepository.GetByID(pro.Id);

                ord.OrderStartus = OrderStatus.Processiong;
                unitOfWork.OrderRepository.Update(ord);
                
             
                pro.Order = ord;
                pro.Costs = ord.Total;
                IEnumerable<ApplicationUser> them =  unitOfWork.UserRepository.Get().Where(s => s.RoleName.Equals(RolesConst.MANAGER));
                foreach (ApplicationUser manager in them)
                {
                    if (manager.UserName.Equals(pro.NameProjectManager))
                        pro.ProjectManager = manager;
                }
              
                pro.ProjectStatus = ProjectStatus.Initial;

                unitOfWork.ProjectRepository.Insert(pro);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

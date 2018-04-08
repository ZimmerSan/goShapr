using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.entities;
using GoSharpProject.Models.repository;

namespace GoSharpProject.Controllers
{
    public class ManagementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        private IEnumerable<Order> activeOrders;

        public ActionResult Index()
        {
            activeOrders = unitOfWork.OrderRepository.Get().Where(s => s.OrderStatus.Equals(OrderStatus.Initiating));
            return View(activeOrders);
        }

        public ActionResult RejectOrder(int? id)
        {
            Order ord = unitOfWork.OrderRepository.GetByID(id);
            ord.OrderStatus = OrderStatus.Rejected;
            unitOfWork.OrderRepository.Update(ord);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


        public ActionResult ConfirmOrder(int orderId)
        {

            IEnumerable<ApplicationUser> managers = unitOfWork.UserRepository.Get().Where(s => s.RoleName.Equals(RolesConst.MANAGER));

            ViewBag.pm = managers;
            //make enumerable from enum
            //ViewBag.ps = (IEnumerable<ProjectStatus>)Enum.GetValues(typeof(ProjectStatus)); 
            Project proj = new Project(){Id = orderId};

            return View(proj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder(Project pro)
        {
            if (ModelState.IsValid)
            {
                Order ord = unitOfWork.OrderRepository.GetByID(pro.Id);

                ord.OrderStatus = OrderStatus.Processing;
                unitOfWork.OrderRepository.Update(ord);


                pro.Order = ord;
                pro.Costs = ord.Total;
                IEnumerable<ApplicationUser> them = unitOfWork.UserRepository.Get().Where(s => s.RoleName.Equals(RolesConst.MANAGER));
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
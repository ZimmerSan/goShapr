using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models;
using GoSharpProject.Models.entities;
using System.Data;
using GoSharpProject.Models.constants;
using GoSharpProject.Models.repository;
using GoSharpProject.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace GoSharpProject.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            UserDashboardViewModel viewModel = new UserDashboardViewModel();
            viewModel.User = unitOfWork.UserRepository.GetByID(User.Identity.GetUserId());

            if (User.IsInRole(RolesConst.ADMIN) || User.IsInRole(RolesConst.MANAGER))
            {
                viewModel.OrdersToBeApproved = unitOfWork.OrderRepository.dbSet.Where(o => o.OrderStatus == OrderStatus.Initiating).ToList();
            }

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

   
    }
}
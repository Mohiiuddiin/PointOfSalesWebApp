using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;

namespace PointOfSalesWebApp.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}
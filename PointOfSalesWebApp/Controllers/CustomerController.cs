using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointOfSalesWebApp.Controllers
{
    public class CustomerController : Controller
    {
        
        IRepository<Customer> context;
        //IRepository<Position> positionRepository;

        public CustomerController(IRepository<Customer> CustomerRepo)
        {
            this.context = CustomerRepo;           

        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = context.Collection().ToList();
            return View(customers);
        }
    }
}
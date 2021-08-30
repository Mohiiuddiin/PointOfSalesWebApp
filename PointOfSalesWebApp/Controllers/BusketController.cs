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
    public class BusketController : Controller
    {
        public IBusketManager BusketManager { get; set; }
        public IOrderManager OrderManager { get; set; }
        public IRepository<Customer> Customers { get; set; }

        public BusketController(IBusketManager busketManager, IOrderManager orderManager, IRepository<Customer> Customers)
        {
            this.BusketManager = busketManager;
            this.OrderManager = orderManager;
            this.Customers = Customers;
        }

        public ActionResult Index()
        {
            var busketItems = BusketManager.GetBusketItems(this.HttpContext);
            return View(busketItems);
        }

        public ActionResult AddToBusket(string id)
        {
            BusketManager.AddBusket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBusket(string id)
        {
            BusketManager.RemoveBusket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BusketSummary()
        {
            var busketSummary = BusketManager.GetBusketSummary(this.HttpContext);
            return PartialView(busketSummary);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CheckOut()
        {
            Customer customer = Customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    State = customer.State,
                    ZipCode = customer.ZipCode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            var busket = BusketManager.GetBusketItems(this.HttpContext);
            order.OrderStatus = "Order Confirmed";
            order.Email = User.Identity.Name;
            //process payment
            order.OrderStatus = "Payment Processed";
            OrderManager.CreateOrder(order, busket);
            BusketManager.ClearBusket(this.HttpContext);
            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.Orderid = OrderId;
            return View();
        }

    }
}
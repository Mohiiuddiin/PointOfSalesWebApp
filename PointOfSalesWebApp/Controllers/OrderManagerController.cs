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
    public class OrderManagerController : Controller
    {
        IOrderManager OrderManager { get; set; }

        public OrderManagerController(IOrderManager OrderManager)
        {
            this.OrderManager = OrderManager;
        }
        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = OrderManager.GetOrderList();
            return View(orders);
        }
        [HttpGet]
        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>
            {
                "Order Created",
                "Order Shipped",
                "Payment Processed",
                "Order Completed"
            };
            Order order = OrderManager.GetOrder(Id);
            return View(order);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order UpdatedOrder, string Id)
        {
            Order order = OrderManager.GetOrder(Id);

            order.OrderStatus = UpdatedOrder.OrderStatus;
            OrderManager.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}
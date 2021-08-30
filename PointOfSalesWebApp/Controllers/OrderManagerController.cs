using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;

namespace PointOfSalesWebApp.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderManager OrderManager { get; set; }
        IRepository<OrderItem> OrderItemRepository;

        public OrderManagerController(IOrderManager OrderManager, IRepository<OrderItem> OrderItemRepository)
        {
            this.OrderManager = OrderManager;
            this.OrderItemRepository = OrderItemRepository;
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
                "Order Confirmed",
                "Order Shipped",
                "Payment Processed",
                "Order Completed"
            };
            ViewBag.PaymentMethodList = new List<string>
            {
                "Cash",
                "Bkash",
                "Nagad"
            };
            Order order = OrderManager.GetOrder(Id);
            ViewBag.OrderItems = OrderItemRepository.Collection().Where(x => x.OrderId == Id).ToList();
            return View(order);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order UpdatedOrder, string Id)
        {
            Order order = OrderManager.GetOrder(Id);

            order.OrderStatus = UpdatedOrder.OrderStatus;
            order.PaymentMethod = UpdatedOrder.PaymentMethod;
            order.TransNo = UpdatedOrder.TransNo;
            order.TransAmount = UpdatedOrder.TransAmount;
            OrderManager.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.ViewModels;

namespace PointOfSalesWebApp.BLL.Manager
{
    public class OrderManager : IOrderManager
    {
        IRepository<Order> orderContext;

        public OrderManager(IRepository<Order> orderContext)
        {
            this.orderContext = orderContext;
        }

        public void CreateOrder(Order order, List<BusketItemView> busketItems)
        {
            foreach (var item in busketItems)
            {
                order.OrderItems.Add(new OrderItem()
                {
                    OrderId = order.Id,
                    ProductId = item.Id,
                    ProductName = item.ProductName,
                    Image = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity
                });

                orderContext.Insert(order);
                orderContext.Commit();
            }
        }

        public List<Order> GetOrderList()
        {
            return orderContext.Collection().ToList();
        }
        public Order GetOrder(string Id)
        {
            return orderContext.Find(Id);
        }
        public void UpdateOrder(Order updatedOrder)
        {
            orderContext.Update(updatedOrder);
            orderContext.Commit();
        }
    }
}

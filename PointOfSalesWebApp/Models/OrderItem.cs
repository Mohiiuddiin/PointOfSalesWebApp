using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Models
{
    public class OrderItem : BaseEntity
    {
        //public Order Order { get; set; }
        public string OrderId { get; set; }
        //public Product Product { get; set; }        
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

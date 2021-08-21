using System;
using System.Collections.Generic;
using PointOfSalesWebApp.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesWebApp.Models
{
    public class BusketItem : BaseEntity
    {
        public string BusketId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

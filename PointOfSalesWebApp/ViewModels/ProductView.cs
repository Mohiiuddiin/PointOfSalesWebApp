using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.ViewModels
{
    public class ProductView
    {
        public Product Product { get; set; }
        public List<ProductCompany> Companies { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}
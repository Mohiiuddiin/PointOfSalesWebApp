using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.ViewModels
{
    public class ProductListView
    {
        public List<Product> Products { get; set; }
        public List<ProductCompany> ProductCompany { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }
    }
}
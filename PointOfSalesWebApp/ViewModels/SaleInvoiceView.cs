using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.ViewModels
{
    public class SaleInvoiceView
    {
        public SaleInvoiceView()
        {
            salesInfoView = new SalesInfoView();
            salesInfoListView = new List<SalesInfoListView>();
        }

        public SalesInfoView salesInfoView { get; set; }
        public List<SalesInfoListView> salesInfoListView { get; set; }
    }
}
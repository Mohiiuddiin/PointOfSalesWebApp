using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class SalesReport
    {
        public string Id  { get; set; }
        public string SalesInvoiceNo { get; set; }
        public string CustomerId { get; set; }        
        public string CustomerName { get; set; }        
        public string Remarks { get; set; }
        public double OverallDiscount { get; set; }
        public string SaleId { get; set; }       
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int BarCode { get; set; }
        public int Quantity { get; set; }
        public double SRate { get; set; }
        public double Vat { get; set; }
        public double LineDiscount { get; set; }
    }
}
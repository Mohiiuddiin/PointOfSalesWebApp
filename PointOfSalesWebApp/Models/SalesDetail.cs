using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class SalesDetail:BaseEntity
    {
        public Sale Sales { get; set; }
        public string SaleId { get; set; }
        public Product Product { get; set; }
        [Required(ErrorMessage = "Please choose a product")]
        public string ProductId { get; set; }
        public int BarCode { get; set; }
        public int Quantity { get; set; }
        public double SRate { get; set; }
        public double Vat { get; set; }
        public double LineDiscount { get; set; }
    }
}
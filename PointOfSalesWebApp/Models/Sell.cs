using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class Sell : BaseEntity
    {       
        [Required]
        public string SalesInvoiceNo { get; set; }

        public Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Customer Customer { get; set; }
        [Required]
        public string CustomerId { get; set; }

        public Employee Employee { get; set; }
        [Required]
        public string EmployeeId { get; set; }

        public string Remarks { get; set; }
        [Required]
        public double OverallDiscount { get; set; }      

        public int BarCode { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public double SRate { get; set; }
        [Required]
        public double Vat { get; set; }
        [Required]
        public double LineDiscount { get; set; }
    }
}
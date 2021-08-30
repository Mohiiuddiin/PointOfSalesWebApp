using PointOfSalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class Sale:BaseEntity
    {
        [Required(ErrorMessage = "Please enter an invoice number")]
        [StringLength(100)]
   
        public string SalesInvoiceNo { get; set; }

        public Customer Customer { get; set; }
        [Required(ErrorMessage = "Please choose a Customer")]
        public string CustomerId { get; set; }

        //public Employee Employee { get; set; }
        //[Required(ErrorMessage = "Please choose a Customer")]
        //public string EmployeeId { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }

        public double OverallDiscount { get; set; }
        public virtual ICollection<SalesDetail> SalesDetailses { get; set; }
    }
}
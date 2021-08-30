using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class Purchase : BaseEntity
    {

        [Required(ErrorMessage = "Please enter an invoice number")]
        [StringLength(100)]
        public string InvoiceNo { get; set; }

        public Supplier Supplier{ get; set; }
        [Required(ErrorMessage = "Please choose a supplier")]
        public string SupplierId { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
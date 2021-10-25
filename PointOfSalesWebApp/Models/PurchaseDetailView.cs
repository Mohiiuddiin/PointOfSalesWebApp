namespace PointOfSalesWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //[Table("PurchaseDetailView")]
    public class PurchaseDetailView
    {
        public string InvoiceNo { get; set; }

        public double PaidAmount { get; set; }

        
        public double DueAmount { get; set; }

        
        public DateTimeOffset CreatedAt { get; set; }

        public string ProductName { get; set; }

        
        public int BarCode { get; set; }

        
        public int Quantity { get; set; }

        
        public int StockQuantity { get; set; }

        
        public double PRate { get; set; }

        
        public double SRate { get; set; }

        [Display(Name="Supplier Name")]
        public string Name { get; set; }
    }
}

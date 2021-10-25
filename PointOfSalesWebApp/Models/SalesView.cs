namespace PointOfSalesWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public class SalesView
    {
        
        public string SalesInvoiceNo { get; set; }

        
        public int Quantity { get; set; }

        
        public double SRate { get; set; }

       
        public double Vat { get; set; }

        
        public double LineDiscount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name="Product")]
        public string Name { get; set; }

        public string CreatedBy { get; set; }


        public DateTimeOffset CreatedAt { get; set; }
    }
}

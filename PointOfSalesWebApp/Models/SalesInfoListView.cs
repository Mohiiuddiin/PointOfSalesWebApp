namespace PointOfSalesWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesInfoListView")]
    public partial class SalesInfoListView
    {
       
        public string SalesInvoiceNo { get; set; }

        
        public int Quantity { get; set; }

       
        public double SRate { get; set; }

        
        public double Vat { get; set; }

        
        public double LineDiscount { get; set; }

        
        public string ProductCode { get; set; }

        
        public string ProductCompanyId { get; set; }

        
        public string ProductName { get; set; }
    }
}

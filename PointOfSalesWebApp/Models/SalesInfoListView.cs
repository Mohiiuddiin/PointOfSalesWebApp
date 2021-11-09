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
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string SalesInvoiceNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        [Key]
        [Column(Order = 2)]
        public double SRate { get; set; }

        [Key]
        [Column(Order = 3)]
        public double Vat { get; set; }

        [Key]
        [Column(Order = 4)]
        public double LineDiscount { get; set; }

        [Key]
        [Column(Order = 5)]
        public string ProductCode { get; set; }

        [Key]
        [Column(Order = 6)]
        public string ProductCompanyId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(20)]
        public string ProductName { get; set; }

        [Key]
        [Column(Order = 8)]
        public DateTimeOffset CreatedAt { get; set; }
        public double Total { get; set; }
    }
}

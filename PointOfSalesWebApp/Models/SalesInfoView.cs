namespace PointOfSalesWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesInfoView")]
    public partial class SalesInfoView
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string SalesInvoiceNo { get; set; }

        [Key]
        [Column(Order = 1)]
        public double OverallDiscount { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}

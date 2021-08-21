using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PointOfSalesWebApp.Models
{
    public class ProductCategory:BaseEntity
    {
        [Required]
        public string Category { get; set; }
        public string Description { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PointOfSalesWebApp.Models
{
    public class ProductCompany:BaseEntity
    {
        [Required]
        public string Company { get; set; }
        public string Description { get; set; }       
    }
}
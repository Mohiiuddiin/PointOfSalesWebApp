using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class Position:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
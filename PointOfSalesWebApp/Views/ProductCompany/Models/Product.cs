using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        public string ProductCode { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double WholeSalePrice { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }
        [Required]
        [DisplayName("Product Category")]
        public string ProductCategoryId { get; set; }
        
        public ProductCompany ProductCompany { get; set; }
        [Required]
        [DisplayName("Product Company")]
        public string ProductCompanyId { get; set; }


        public string Image { get; set; }


        public Product()
        {
            Quantity = 0;
        }

    }
}
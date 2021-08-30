using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class Supplier : BaseEntity
    {
      

        [Required(ErrorMessage = "Supplier name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [RegularExpression(@"^([0-9]+)*$", ErrorMessage = "Only Numbers are allowed!")]
        [Required(ErrorMessage = "Phone Num is required")]
        [Display(Name = "Phone Number")]
        [StringLength(20)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please Input Valid Email Address")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage="Address is required")]
        [StringLength(100)]
        public string Address { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
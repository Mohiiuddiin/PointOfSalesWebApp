using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Models
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.Name = this.FirstName + this.LastName;
        }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string MobileNumber { get; set; }
    }
}

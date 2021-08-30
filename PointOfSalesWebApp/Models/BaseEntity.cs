using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;



namespace PointOfSalesWebApp.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }




        public BaseEntity()
        {
            this.CreatedBy = HttpContext.Current.User.Identity.Name;
            this.ApplicationUserId = HttpContext.Current.User.Identity.GetUserId();
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
            this.Status = 1;
        }
    }
}
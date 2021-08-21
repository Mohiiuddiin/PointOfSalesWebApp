using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class Attendance : BaseEntity
    {
        //        (ID, EmployeeID, CheckTime, IsCheckIn, IsCheckOut,
        //ImageUrl, Latitude, Longitude, Note)
        public Employee Employee { get; set; }
        [Required]
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }
        [Required]
        [Display(Name = "Check Time")]
        public DateTime CheckTime { get; set; }
        [Required]
        [Display(Name = "Check Status")]
        public string CheckStatus { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public string Note { get; set; }




        public Attendance()
        {
            this.CheckTime = DateTime.Now;

            //GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            //// Do not suppress prompt, and wait 1000 milliseconds to start.
            //watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
            //GeoCoordinate coord = watcher.Position.Location;
            //if (coord.IsUnknown != true)
            //{
            //    Latitude = coord.Latitude.ToString();
            //    Longitude = coord.Longitude.ToString();
            //}
            //else
            //{
            //    Latitude = "Not Found";
            //    Longitude = "Not Found";
            //}
        }
    }
}
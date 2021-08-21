using PointOfSalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.ViewModels
{
    public class EmployeeView
    {
        public Employee Employee { get; set; }
        public List<Position> Positions { get; set; }
    }
}
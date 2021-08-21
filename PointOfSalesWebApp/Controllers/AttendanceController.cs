using PointOfSalesWebApp.DAL.Gateway;
using PointOfSalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PointOfSalesWebApp.Controllers
{
    public class AttendanceController : Controller
    {
        public ApplicationDbContext db;
        public SqlRepository<Attendance> context;
        public SqlRepository<Employee> employeeContext;

        public AttendanceController(SqlRepository<Attendance> attendanceContext, SqlRepository<Employee> employeeContext)
        {
            context = attendanceContext;
            this.employeeContext = employeeContext;
        }
        public ActionResult Index()
        {
            List<Attendance> attendances = context.Collection().Include(x=>x.Employee).ToList();

            return View(attendances);
        }
        // GET: Attendence
        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]

        public ActionResult Create()
        {
            Attendance attendance = new Attendance();
            ViewBag.Employee = employeeContext.Collection().Select(x => new { x.Id }).ToList();

            return View(attendance);
        }
        [HttpPost]

        public ActionResult Create(Attendance attendance, string ImageUrl)
        {
            ViewBag.Employee = employeeContext.Collection().Select(x => new { x.Id }).ToList();
            Attendance lastEmployeeCheckedbyId = context.Collection()
                        .Where(m => m.EmployeeId == attendance.EmployeeId)
                        .OrderByDescending(m => m.CheckTime)
                        .FirstOrDefault();
            if (lastEmployeeCheckedbyId != null)
            {
                if (lastEmployeeCheckedbyId.CheckTime.Day == attendance.CheckTime.Day)
                {
                    ViewBag.Message = "Sorry! You Have already Checked,try from another day";
                    return View();
                }
            }
            string fileName = DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + ".jpeg";
            SaveDataUrlToFile(ImageUrl, fileName);

            attendance.ImageUrl = fileName;
            //Server.MapPath(string.Format("{0}.jpeg", fileName))
            //System.IO.File.WriteAllText(Server.MapPath("~/Content/Images/" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt"), str);          
            //~/Content/Images/

            context.Insert(attendance);
            context.Commit();

            ViewBag.Message = "Save Successful";
            return View();
        }
        public void SaveDataUrlToFile(string dataUrl, string filename)
        {
            var matchGroups = Regex.Match(dataUrl, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
            var base64Data = matchGroups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            System.IO.File.WriteAllBytes(Server.MapPath("//Content//EmployeeImages//" + filename), binData);
            //return savePath;
        }
    }
}
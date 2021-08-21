using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace PointOfSalesWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        IRepository<Employee> context;
        IRepository<Position> positionRepository;

        public EmployeeController(IRepository<Employee> employeeRepo, IRepository<Position> positionRepo)
        {
            this.context = employeeRepo;
            this.positionRepository = positionRepo;
            
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //protected override void Dispose(bool disposing)
        //{
        //    cx.Dispose();
        //}
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult AddPosition()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPosition(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View(position);
            }
            else
            {
                positionRepository.Insert(position);
                positionRepository.Commit();

                return View(position);
            }
        }

        // GET: Employee
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Index()
        {
            List<Employee> employees = context.Collection().Include(x => x.Position).ToList();

            return View(employees);
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterEmployee()
        {         
                return View();            
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> RegisterEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                user.Id = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //register customer model
                    await UserManager.AddToRoleAsync(user.Id, "Employee");

                    Employee employee = new Employee()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        City = model.City,
                        State = model.State,
                        ZipCode = model.ZipCode,
                        MobileNumber = model.MobileNumber,
                        Id = user.Id
                    };
                    
                    context.Insert(employee);
                    context.Commit();
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string Id)
        {
            ViewBag.Positions = positionRepository.Collection().ToList();

            Employee employee = context.Find(Id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            else
            {
                //var viewModel = context.Collection().ToList();

                return View(employee);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(Employee employee, string Id, HttpPostedFileBase file)
        {
            ViewBag.Positions = positionRepository.Collection().ToList();

            Employee employeeToEdit = context.Find(Id);

            if (employeeToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(employee);
                }

                if (file != null)
                {
                    employeeToEdit.Image = employee.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//EmployeeImages//") + employeeToEdit.Image);
                }
                employeeToEdit.FirstName = employee.FirstName;
                employeeToEdit.LastName = employee.LastName;
                employeeToEdit.MobileNumber = employee.MobileNumber;
                employeeToEdit.Email = employee.Email;
                employeeToEdit.City = employee.City;
                employeeToEdit.PositionId = employee.PositionId;
                employeeToEdit.State = employee.State;



                context.Commit();

                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string Id)
        {
            Employee employeeToDelete = context.Find(Id);

            if (employeeToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(employeeToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmDelete(string Id)
        {
            Employee employeeToDelete = context.Find(Id);

            if (employeeToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


    }
}
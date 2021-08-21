using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointOfSalesWebApp.DAL.Gateway;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Controllers
{

    public class ProductCompanyController : Controller
    {

        private IRepository<ProductCompany> repository;

        public ProductCompanyController(IRepository<ProductCompany> repository)
        {
            this.repository = repository;
        }

        // GET: Company
        [Authorize(Roles = "Admin,Employee")]

        public ActionResult Index()
        {
            List<ProductCompany> productCompany = repository.Collection().ToList();
            return View(productCompany);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]

        public ActionResult Create()
        {
            ProductCompany productCompany = new ProductCompany();
            return View(productCompany);
        }
        [HttpPost]
        public ActionResult Create(ProductCompany productCompany)
        {
            if (!ModelState.IsValid)
            {
                return View(productCompany);
            }
            else
            {
                repository.Insert(productCompany);
                repository.Commit();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]

        public ActionResult Edit(string Id)
        {
            ProductCompany productCompany = repository.Find(Id);
            if (productCompany == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCompany);
            }

        }
        [HttpPost]
        public ActionResult Edit(ProductCompany productCompany)
        {
            if (productCompany == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCompany);
                }
                else
                {
                    repository.Update(productCompany);
                    repository.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Delete(string Id)
        {
            ProductCompany productCompany = repository.Find(Id);

            if (productCompany != null)
            {
                repository.Delete(Id);
                repository.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}
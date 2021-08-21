using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointOfSalesWebApp.Controllers
{
    public class StockController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<ProductCompany> productCompanies;
        //public ApplicationDbContext dbContext;

        public StockController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext, IRepository<ProductCompany> productCompanyContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
            productCompanies = productCompanyContext;
            //dbContext = new ApplicationDbContext();
        }
        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            Product product = new Product();
            ViewBag.companies = productCompanies.Collection().ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Add(Product product)
        {
            ViewBag.companies = productCompanies.Collection().ToList();

            Product productToEdit = context.Find(product.Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(product);
                //}                
                //productToEdit.ProductCompany = product.ProductCompany;
                //productToEdit.Category = product.Category;
                //productToEdit.Description = product.Description;
                //productToEdit.Name = product.Name;
                //productToEdit.Price = product.Price;
                //productToEdit.ProductCode = product.ProductCode;
                //productToEdit.WholeSalePrice = product.WholeSalePrice;
                productToEdit.Quantity = product.Quantity;



                context.Commit();
                ViewBag.Message = "Increased Stock Successfully";
                return View();
            }
        }
        public JsonResult GetProductsByCompany(string id)
        {
            var products = context.Collection().Where(x => x.ProductCompanyId == id).ToList();
            return Json(products);
        }
        public JsonResult GetProductById(string id)
        {
            Product product = context.Collection().Single(x => x.Id == id);
            product.Category = productCategories.Collection().Single(x=>x.Id==product.ProductCategoryId);
            product.ProductCompany = productCompanies.Collection().Single(x=>x.Id==product.ProductCompanyId);
            product.Image = "~/Content/ProductImages/" + product.Image;
            
            return Json(product);
        }
        //GetProductById

    }
}
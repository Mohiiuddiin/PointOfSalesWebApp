using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;

namespace PointOfSalesWebApp.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<ProductCompany> productCompanies;
        //public ApplicationDbContext dbContext;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext, IRepository<ProductCompany> productCompanyContext) {
            context = productContext;
            productCategories = productCategoryContext;
            productCompanies = productCompanyContext;
            //dbContext = new ApplicationDbContext();
        }
        [Authorize(Roles = "Admin,Employee")]

        public ActionResult Index()
        {
            
            List<Product> products = context.Collection().Include(x=>x.Category).Include(x=>x.ProductCompany).ToList();
            return View(products);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]

        public ActionResult Create() {
            ProductView viewModel = new ProductView();

            viewModel.Product = new Product();
            viewModel.Categories = productCategories.Collection().ToList();
            viewModel.Companies = productCompanies.Collection().ToList();
            //Product product = new Product();

            //ViewBag.Categories = dbContext.ProductCategories.ToList();
            //ViewBag.Companies = dbContext.ProductCompanies.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file) {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else {

                if (file != null) {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string Id) {

            ViewBag.Categories = productCategories.Collection().ToList();
            ViewBag.Companies = productCompanies.Collection().ToList();

            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else {
                //ProductView viewModel = new ProductView();
                //viewModel.Product = product;
                //viewModel.Categories = productCategories.Collection().ToList();
                //viewModel.Companies = productCompanies.Collection().ToList();
                


                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            ViewBag.Categories = productCategories.Collection().ToList();
            ViewBag.Companies = productCompanies.Collection().ToList();
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid) {
                    return View(product);
                }

                if (file != null) {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }
                productToEdit.ProductCompanyId = product.ProductCompanyId;
                productToEdit.ProductCategoryId = product.ProductCategoryId;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.ProductCode = product.ProductCode;
                productToEdit.WholeSalePrice = product.WholeSalePrice;
                


                context.Commit();

                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id) {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
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
    }
}
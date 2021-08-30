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




        public ActionResult StockOut()
        {
            ViewBag.companies = productCompanies.Collection().ToList();
            ViewBag.Categories = productCategories.Collection().ToList();
            Product product = new Product();

            return View(product);
        }
        //public ActionResult StockOut()
        //{
        //    ViewBag.companies = productCompanies.Collection().ToList();
        //    ViewBag.Categories = productCategories.Collection().ToList();
        //    Product product = new Product();

        //    return View(product);
        //}

        //public JsonResult GetProductInfo(int id)
        //{
        //    string[] pp = new string[3];
        //    var purchase = db.PurchaseDetails.Where(x => x.BarCode == id).FirstOrDefault();

        //    if (purchase != null)
        //    {
        //        pp[0] = purchase.ProductId.ToString();
        //        pp[1] = purchase.StockQuantity.ToString();
        //        pp[2] = purchase.SRate.ToString();
        //    }

        //    return Json(pp, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult AutoCompleteCountry(string term)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            List<String> result = new List<String>();
            result = db.PurchaseDetails.Where(x => x.BarCode.ToString().StartsWith(term)).Select(y => y.BarCode.ToString()).ToList();

            //var result = db.PurchaseDetails.Where(s => term == null || s.BarCode.ToString().ToLower().Contains(term.ToLower())).Select(x => new { id = x.BarCode, value = x.BarCode }).Take(5).ToList();

            //List<string> result = db.PurchaseDetails.Where(x => x.BarCode.ToString().StartsWith(term)).Select(c => c.BarCode.ToString()).ToList();

            //var result = (from r in db.PurchaseDetails
            //              where r.BarCode.ToString().ToLower().Contains(term.ToLower())
            //              select new { r.BarCode }).Distinct();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveSales(Sale objSales)
        {

            bool status = false;
            if (ModelState.IsValid)
            {
                List<Product> productList = new List<Product>();
                List<PurchaseDetail> purchaseDetailsList = new List<PurchaseDetail>();

                using (ApplicationDbContext dc = new ApplicationDbContext())
                {
                    foreach (SalesDetail p in objSales.SalesDetailses)
                    {
                        PurchaseDetail purchaseDetail = dc.PurchaseDetails.Where(x => x.BarCode == p.BarCode).FirstOrDefault();
                        purchaseDetail.StockQuantity = purchaseDetail.StockQuantity - p.Quantity;

                        Product product = dc.Products.Where(x => x.Id == p.ProductId).FirstOrDefault();
                        product.Quantity = product.Quantity - p.Quantity;

                        productList.Add(product);
                        purchaseDetailsList.Add(purchaseDetail);
                    }
                    dc.Sales.Add(objSales);
                    foreach (var p in productList)
                    {
                        dc.Entry(p).State = EntityState.Modified;
                    }

                    foreach (var p in purchaseDetailsList)
                    {
                        dc.Entry(p).State = EntityState.Modified;
                    }

                    dc.SaveChanges();
                    status = true;
                }
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
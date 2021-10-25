using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointOfSalesWebApp.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace PointOfSalesWebApp.Controllers
{
    public class PurchaseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Purchase
        //PurchaseDetailSp

        public ActionResult PurchasesView()
        {
            List<PurchaseDetailView> result = db.Database.SqlQuery<PurchaseDetailView>("exec PurchaseDetailSp").ToList();

            return View(result);
        }
        public ActionResult Index()
        {
            ViewBag.Suppliers = db.Suppliers.ToList();
            

            PurchaseDetail purchaseDetail = new PurchaseDetail();
            ViewBag.BarcodeGenerated = purchaseDetail.BarCode;

            ViewBag.Products = db.Products.ToList();
            
            return View();
        }

        [HttpPost]
        public JsonResult SaveOrder(Purchase purchase)
        {
            
            bool status = false;
            if (ModelState.IsValid)
            {
                string currentUser = User.Identity.GetUserId();

                purchase.CreatedBy = currentUser;
                
                List<Product> productList = new List<Product>();
                using (ApplicationDbContext dc = new ApplicationDbContext())
                {
                    foreach (PurchaseDetail p in purchase.PurchaseDetails)
                    {
                        Product product = dc.Products.Where(x => x.Id == p.ProductId).FirstOrDefault();
                        product.Quantity = product.Quantity + p.Quantity;
                        p.StockQuantity = p.StockQuantity + p.Quantity;
           
                        productList.Add(product);
                    }


                    dc.Purchases.Add(purchase);
                    foreach (var p in productList)
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
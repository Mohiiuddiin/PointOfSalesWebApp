﻿using Microsoft.AspNet.Identity;
using PointOfSalesWebApp.DAL.Gateway;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PointOfSalesWebApp.Controllers
{
    public class SalesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public SqlRepository<SalesDetail> _SalesContext;

        public SalesController(SqlRepository<SalesDetail> SalesRepo)
        {
            this._SalesContext = SalesRepo;
        }
        // GET: Purchase
        public ActionResult Index(int ProductId = 0)
        {
            var customerList = db.Customers.ToList();
            ViewBag.Customers = customerList;

            var productList = db.Products.ToList();
            ViewBag.Products = productList;
            return View();
        }

        public ActionResult SalesView()
        {
           List<SalesView> salesView = db.Database.SqlQuery<SalesView>("exec SalesDetailViewSp").ToList();


            return View(salesView);
        }

        //SalesInfoInvoice
        public ActionResult SalesInfoInvoice(string Inv)
        {

            SaleInvoiceView saleInvoiceView = new SaleInvoiceView();
            SqlParameter InvParam = new SqlParameter("@Inv", Inv);
            SqlParameter InvParam1 = new SqlParameter("@Inv", Inv);

            saleInvoiceView.salesInfoView = db.Database.SqlQuery<SalesInfoView>("exec SalesInfoViewSp @Inv", InvParam).FirstOrDefault();
            saleInvoiceView.salesInfoListView = db.Database.SqlQuery<SalesInfoListView>("exec SalesInfoListByInvSp @Inv", InvParam1).ToList();


            return View(saleInvoiceView);
        }
        [HttpGet]
        public JsonResult GetProductInfo(int id)
        {
            string[] pp = new string[3];
            var purchase = db.PurchaseDetails.Where(x => x.BarCode == id).Include(x => x.Product).FirstOrDefault();

            if (purchase != null)
            {
                pp[0] = purchase.ProductId.ToString();
                //pp[1] = purchase.StockQuantity.ToString();
                pp[1] = purchase.Product.Quantity.ToString();
                pp[2] = purchase.SRate.ToString();
            }

            return Json(pp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutoCompleteCountry(string term)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            List<String> result = new List<String>();
            result = db.PurchaseDetails.Where(x => x.BarCode.ToString().StartsWith(term)).Select(y => y.BarCode.ToString()).ToList();


            return Json(result, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult SaveSales(Sale objSales)
        {

            bool status = false;
            if (true)
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
                    objSales.ApplicationUserId = User.Identity.GetUserId();
                    objSales.CreatedBy = User.Identity.GetUserName();
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




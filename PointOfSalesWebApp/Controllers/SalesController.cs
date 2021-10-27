using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using PointOfSalesWebApp.DAL.Gateway;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
        public ActionResult ViewReport(string Inv)
        {
            SalesInfoViewContext salesct = new SalesInfoViewContext();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;


            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            try
            {
                SalesInfoView data = new SalesInfoView();
                IEnumerable<SalesInfoListView> dataList = new List<SalesInfoListView>();

                //SaleInvoiceView saleInvoiceView = new SaleInvoiceView();
                //SqlParameter InvParam = new SqlParameter("@Inv", Inv);
                //SqlParameter InvParam1 = new SqlParameter("@Inv", Inv);

                //data = db.Database.SqlQuery<SalesInfoView>("exec SalesInfoViewSp @Inv", InvParam).FirstOrDefault();
                //dataList = db.Database.SqlQuery<SalesInfoListView>("exec SalesInfoListByInvSp @Inv", InvParam1).ToList();
                data = salesct.SalesInfoViews.FirstOrDefault(x => x.SalesInvoiceNo == Inv);
                dataList = salesct.SalesInfoListViews.Where(x => x.SalesInvoiceNo == Inv).ToList();
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CustomerInvoice.rdlc";
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsSalesInfo", data));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsSalesListInfo", dataList));
                ViewBag.ReportViewer = reportViewer;
            }
            catch (Exception ex)
            {
                throw;
            }


            return View();
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




            SaleInvoiceView saleInvoiceView = new SaleInvoiceView();
            SqlParameter InvParam = new SqlParameter("@Inv", objSales.SalesInvoiceNo);
            SqlParameter InvParam1 = new SqlParameter("@Inv", objSales.SalesInvoiceNo);

            saleInvoiceView.salesInfoView = db.Database.SqlQuery<SalesInfoView>("exec SalesInfoViewSp @Inv", InvParam).FirstOrDefault();
            saleInvoiceView.salesInfoListView = db.Database.SqlQuery<SalesInfoListView>("exec SalesInfoListByInvSp @Inv", InvParam1).ToList();

            var senderEmail = new MailAddress("mdmohiuddin050505@gmail.com", "Mohiuddin");
            //var receiverEmail = new MailAddress("md.mohiiuddiin@gmail.com", "Receiver");
            var receiverEmail = new MailAddress(saleInvoiceView.salesInfoView.Email, "Receiver");
            var password = "73564681";
            var sub = "Invoice of Purchased Product";
            var body = "Hello"+" "+ saleInvoiceView.salesInfoView.FirstName+" "+ saleInvoiceView.salesInfoView.LastName+",\n"+
                "Invoice Information:"+"\n\n"+"Inv No:"+ saleInvoiceView.salesInfoView.SalesInvoiceNo
                +"\nProduct Information:\n";
            int count = 0;
            double sum = 0;
            foreach (var item in saleInvoiceView.salesInfoListView)
            {
                count++;
                body += @count.ToString()+". "+item.ProductName+", Product Code :"+item.ProductCode+", Quantity:"+item.Quantity+",Price:"+item.SRate+"\n\n";
                sum += (item.Quantity + item.SRate);

            }

            body += "Discount:"+ saleInvoiceView.salesInfoView.OverallDiscount+"%\n\n";
            body += "Total:"+ sum+"\n\n";
            body += "Thank You";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };

            smtp.EnableSsl = true;
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}




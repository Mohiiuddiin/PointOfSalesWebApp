using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;

namespace PointOfSalesWebApp.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<ProductCompany> productCompanies;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext, IRepository<ProductCompany> productCompanyContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
            productCompanies = productCompanyContext;
        }
        public ActionResult Index(string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            List<ProductCompany> companies = productCompanies.Collection().ToList();
            if(Category == null)
            {
                products = context.Collection().Include(x=>x.ProductCompany).ToList();
            }
            else
            {
                products = context.Collection().Where(x => x.Category.Category == Category).Include(x => x.ProductCompany).ToList();
            }

            ProductListView productList = new ProductListView
            {
                Products = products,
                ProductCategory = categories,
                ProductCompany = companies
            };

            return View(productList);
        }
        public ActionResult Details(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
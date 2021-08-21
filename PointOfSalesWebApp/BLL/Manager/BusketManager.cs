using PointOfSalesWebApp.Interfaces;
using PointOfSalesWebApp.Models;
using PointOfSalesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PointOfSalesWebApp.BLL.Manager
{
    public class BusketManager : IBusketManager
    {
        IRepository<Product> productContext;
        IRepository<Busket> busketContext;

        public const string BusketSessionName = "eCommerceBusket";
        public BusketManager(IRepository<Product> productContext,IRepository<Busket> busketContext)
        {
            this.productContext = productContext;
            this.busketContext = busketContext;
        }

        private Busket GetBusket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BusketSessionName);
            Busket busket = new Busket();

            if (cookie != null)
            {
                string busketId = cookie.Value;
                if (!string.IsNullOrEmpty(busketId))
                {
                    busket = busketContext.Find(busketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        busket = CreateNewBusket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    busket = CreateNewBusket(httpContext);
                }
            }
            return busket;
        }

        private Busket CreateNewBusket(HttpContextBase httpContext)
        {
            Busket busket = new Busket();
            busketContext.Insert(busket);
            busketContext.Commit();
            HttpCookie httpCookie = new HttpCookie(BusketSessionName);
            httpCookie.Value = busket.Id;
            httpCookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(httpCookie);

            return busket;
        }

        public void AddBusket(HttpContextBase httpContext, string productId)
        {
            
            Busket busket = GetBusket(httpContext, true);
            if (busket == null) {
                busket = new Busket();                
            }
            
            BusketItem busketItem = busket.BusketItems.FirstOrDefault(x => x.ProductId == productId);
            
            if (busketItem == null)
            {
                busketItem = new BusketItem()
                {
                    BusketId = busket.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                busket.BusketItems.Add(busketItem);
            }
            else
            {
                busketItem.Quantity = busketItem.Quantity + 1;
            }
            busketContext.Commit();
        }

        public void RemoveBusket(HttpContextBase httpContext, string itemId)
        {
            Busket busket = GetBusket(httpContext, true);
            BusketItem busketItem = busket.BusketItems.FirstOrDefault(x => x.BusketId == itemId);
            if (busketItem != null)
            {
                busket.BusketItems.Remove(busketItem);
                busketContext.Commit();
            }

        }
        public List<BusketItemView> GetBusketItems(HttpContextBase httpContextBase)
        {
            Busket busket = GetBusket(httpContextBase, false);

            if (busket != null)
            {
                var results = (from b in busket.BusketItems
                               join p in productContext.Collection()
                               on b.ProductId equals p.Id
                               select new BusketItemView()
                               {
                                   Id = b.BusketId,
                                   Quantity = b.Quantity,
                                   ProductName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price,
                               }).ToList();
                return results;
            }
            else
            {
                return new List<BusketItemView>();
            }

        }

        public BusketSummaryView GetBusketSummary(HttpContextBase httpContextBase)
        {
            Busket busket = GetBusket(httpContextBase, false);
            BusketSummaryView busketSummaryView = new BusketSummaryView(0, 0);

            if (busket != null)
            {
                int? busketCount = (from item in busket.BusketItems select item.Quantity).Sum();
                decimal? busketTotal = (from item in busket.BusketItems
                                        join p in productContext.Collection()
        on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();

                busketSummaryView.BusketCount = busketCount ?? 0;
                busketSummaryView.BusketTotal = busketTotal ?? decimal.Zero;

                return busketSummaryView;
            }
            else
            {
                return busketSummaryView;
            }
        }
        public void ClearBusket(HttpContextBase httpContextBase)
        {
            Busket busket = GetBusket(httpContextBase, false);
            if(busket != null)
            {
                busket.BusketItems.Clear();
                busketContext.Commit();
            }
        }    


    }
}

using PointOfSalesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PointOfSalesWebApp.Interfaces
{
    public interface IBusketManager
    {
        void AddBusket(HttpContextBase httpContext, string productId);
        void RemoveBusket(HttpContextBase httpContext, string itemId);
        List<BusketItemView> GetBusketItems(HttpContextBase httpContextBase);
        BusketSummaryView GetBusketSummary(HttpContextBase httpContextBase);
        void ClearBusket(HttpContextBase httpContextBase);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSalesWebApp.ViewModels
{
    public class BusketSummaryView
    {
        public int BusketCount { get; set; }
        public decimal BusketTotal { get; set; }

        public BusketSummaryView()
        {

        }

        public BusketSummaryView(int busketCount, decimal busketTotal)
        {
            this.BusketCount = busketCount;
            this.BusketTotal = busketTotal;
        }
    }
}

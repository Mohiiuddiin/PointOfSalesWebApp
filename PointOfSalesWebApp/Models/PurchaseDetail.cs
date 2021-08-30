using PointOfSalesWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PointOfSalesWebApp.Models
{
    public class PurchaseDetail:BaseEntity
    {
        public PurchaseDetail()
        {
            barcodecs objBar = new barcodecs();
            BarCode = Convert.ToInt32(objBar.generateBarcode());
            BarcodeImage = objBar.getBarcodeImage(objBar.generateBarcode(), ProductName);
        }
     
        public Purchase Purchase { get; set; }

        public string PurchaseId { get; set; }

        public Product Product { get; set; }

        [Required(ErrorMessage = "Please choose a product")]
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int BarCode { get; set; }
        public byte[] BarcodeImage { get; set; }
        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
        public int StockQuantity { get; set; }
        public double PRate { get; set; }
        public double SRate { get; set; }
        public double Vat { get; set; }
        public double Discount { get; set; }
    }
}
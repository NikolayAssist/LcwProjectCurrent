using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.Core.Models
{
    public class OrderBox
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public string GUID { get; set; }
        public int CustomerOrderId { get; set; }
    }
}
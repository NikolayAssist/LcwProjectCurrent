using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.Core.Models
{
    public class CustomerOrderDTO
    {
        public int CustomerOrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string OrderAdress { get; set; }
        public double Amount { get; set; }
        public System.DateTime Date_ { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string GUID { get; set; }

        public ProductDTO Product { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.DTOs
{
    public class CustomerOrderDTO
    {
        public int CustomerOrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string OrderAdress { get; set; }
        public double Amount { get; set; }
        public System.DateTime Date_ { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string Guid { get; set; }
        public ProductDTO Product { get; set; }
        public string ProductName { get; set; }

    }
}
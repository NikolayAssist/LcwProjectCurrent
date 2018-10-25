using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}
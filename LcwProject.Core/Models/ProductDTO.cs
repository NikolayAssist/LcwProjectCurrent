using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LcwProject.Core.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Display(Name = "Barkod No : ")]
        [Required(ErrorMessage ="Barkod Numarası Giriniz..")]
        public string Barcode { get; set; }
        [Display(Name = "Açıklama : ")]
        [Required(ErrorMessage = "Barkod Numarası Giriniz..")]
        public string Description { get; set; }
        [Display(Name = "Miktar : ")]
        public Nullable<double> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}
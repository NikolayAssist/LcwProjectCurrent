using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LcwProject.Core
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Müşteri İsmi Girmelisiniz")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Müşteri Adresi Girmelisiniz")]
        public string CustomerAdress { get; set; }

    }
}
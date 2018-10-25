using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAdress { get; set; }

    }
}
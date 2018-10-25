using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.DTOs
{
    public class CustomerOrderHeaderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderAdress { get; set; }
        public double OrderPrice { get; set; }
        public System.DateTime Date_ { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<CustomerOrderDTO> CustomerOrder { get; set; }
    }
}
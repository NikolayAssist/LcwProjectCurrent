using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace LcwProject.Core.Repositories
{
    public class CustomerRepository
    {
        private static string APIURL = APIInfo.ApiInfo.APIURL;
        public static List<CustomerDTO> GetCustomerList()
        {
            List<CustomerDTO> List = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var Request_ = client.GetAsync(APIURL + "GetAllCustomers").Result;
                if (Request_.IsSuccessStatusCode)
                {
                    List = JsonConvert.DeserializeObject<List<CustomerDTO>>(Request_.Content.ReadAsStringAsync().Result);
                }
            }
            return List;
        }
    }
}
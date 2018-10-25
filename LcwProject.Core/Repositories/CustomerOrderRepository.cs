using LcwProject.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace LcwProject.Core.Repositories
{
    public class CustomerOrderRepository
    {
        private static string APIURL = APIInfo.ApiInfo.APIURL;
        public static void CustomerOrderPost(CustomerOrderHeaderDTO customerOrder)
        {
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var serializedProduct = JsonConvert.SerializeObject(customerOrder);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

                var Result = client.PostAsync(APIURL + "orders/", content).Result;
                if (Result.IsSuccessStatusCode)
                {
                } 
            }
        }
        public static void CustomerOrderPut(CustomerOrderHeaderDTO customerOrder)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var serializedProduct = JsonConvert.SerializeObject(customerOrder);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

                var Result = client.PutAsync(APIURL + "orders/", content);
            }
        }
        public static CustomerOrderHeaderDTO GetOrder(int id)
        {
            CustomerOrderHeaderDTO order = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var requestContent = client.GetAsync(APIURL + "orders/"+ id).Result;
                if (requestContent.IsSuccessStatusCode)
                {
                    order = JsonConvert.DeserializeObject<CustomerOrderHeaderDTO>(requestContent.Content.ReadAsStringAsync().Result);
                }
            }
            return order;
        }
        public static List<CustomerOrderHeaderDTO> GetAllOrderList()
        {
            List<CustomerOrderHeaderDTO> orderList = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var requestContent = client.GetAsync(APIURL + "GetAllOrders/").Result;
                if (requestContent.IsSuccessStatusCode)
                {
                    orderList = JsonConvert.DeserializeObject<List<CustomerOrderHeaderDTO>>(requestContent.Content.ReadAsStringAsync().Result);
                }
            }
            return orderList;
        }
    }
}
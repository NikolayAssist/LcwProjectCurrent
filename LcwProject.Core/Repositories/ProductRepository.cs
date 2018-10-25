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
    public class ProductRepository
    {
        private static string APIURL = APIInfo.ApiInfo.APIURL;
        public static List<ProductDTO> GetProductList()
        {
            List<ProductDTO> List = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var Request_ = client.GetAsync(APIURL + "GetProductList").Result;
                if (Request_.IsSuccessStatusCode)
                {
                    List = JsonConvert.DeserializeObject<List<ProductDTO>>(Request_.Content.ReadAsStringAsync().Result);
                }
            }
            return List;
        }
        public static ProductDTO GetProductById(int id)
        {
            ProductDTO product = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIURL);
                client.DefaultRequestHeaders.Accept.Clear();

                var Request_ = client.GetAsync(APIURL + "GetProductListById/" + id).Result;
                if (Request_.IsSuccessStatusCode)
                {
                    product = JsonConvert.DeserializeObject<ProductDTO>(Request_.Content.ReadAsStringAsync().Result);
                }
            }
            return product;
        }
        public static void PostProduct(ProductDTO product)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIURL);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var serializedProduct = JsonConvert.SerializeObject(product);
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");

                    var request = client.PostAsync(APIURL + "Product/", content).Result;
                    if (request.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public static void DeleteProduct(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    var request = client.DeleteAsync(APIURL + "GetProductListById/" + id).Result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
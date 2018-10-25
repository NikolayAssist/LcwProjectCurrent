using LcwProject.API.DTOs;
using LcwProject.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LcwProject.API.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("GetProductList/")]
        public HttpResponseMessage ProductList()
        {
            HttpResponseMessage message = null;
            try
            {
                ProductRepository repo = new ProductRepository();
                message = Request.CreateResponse(HttpStatusCode.OK, repo.GetProductList());
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }
        [HttpGet]
        [Route("GetProductListById/{id}")]
        public HttpResponseMessage ProductList(int id)
        {
            HttpResponseMessage message = null;
            try
            {
                ProductRepository repo = new ProductRepository();
                message = Request.CreateResponse(HttpStatusCode.OK, repo.GetProductById(id));
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }
        [HttpPost]
        [Route("Product/")]
        public HttpResponseMessage PostProduct(ProductDTO product)
        {
            HttpResponseMessage message = null;
            try
            {
                ProductRepository repo = new ProductRepository();
                repo.PostProduct(product);
                message = Request.CreateResponse(HttpStatusCode.OK, "İşlem Tamamlandı");
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }
        [HttpDelete]
        [Route("GetProductListById/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            HttpResponseMessage message = null;
            try
            {
                ProductRepository repo = new ProductRepository();
                repo.DeleteProduct(id);
                message = Request.CreateResponse(HttpStatusCode.OK, "İşlem Tamamlandı");
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }
    }
}

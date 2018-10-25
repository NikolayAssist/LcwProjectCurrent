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
    public class CustomerOrderController : ApiController
    {
        [HttpPost]
        [Route("orders/")]
        public HttpResponseMessage PostCustomer(CustomerOrderHeaderDTO customerOrderHeaderDTO)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerOrderRepository customerRepo = new CustomerOrderRepository();
                customerRepo.PostOrder(customerOrderHeaderDTO);

                message = Request.CreateResponse(HttpStatusCode.Accepted, "Ekleme İşlem Tamamlandı");

                customerRepo = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }

        [HttpGet]
        [Route("orders/{id}")]
        public HttpResponseMessage GetOrder(int id)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerOrderRepository orderRepo = new CustomerOrderRepository();
                var order = orderRepo.GetOrder(id);

                message = Request.CreateResponse(HttpStatusCode.Accepted, order);
                order = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }

        [HttpPut]
        [Route("orders/{id}")]
        public HttpResponseMessage PutOrder(int id, CustomerOrderHeaderDTO customerOrderHeaderDTO)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerOrderRepository customerRepo = new CustomerOrderRepository();
                customerRepo.PostOrder(customerOrderHeaderDTO);

                message = Request.CreateResponse(HttpStatusCode.Accepted, "Ekleme İşlem Tamamlandı");

                customerRepo = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }

        [HttpDelete]
        [Route("orders/{id}")]
        public HttpResponseMessage DeleteOrder(int id)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerOrderRepository customerRepo = new CustomerOrderRepository();
                customerRepo.DeleteOrder(id);

                message = Request.CreateResponse(HttpStatusCode.Accepted, "Silme Başarılı");
                customerRepo = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public HttpResponseMessage GetAllOrders()
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerOrderRepository customerRepo = new CustomerOrderRepository();
                var OrderList = customerRepo.GetAllOrders();

                message = Request.CreateResponse(HttpStatusCode.Accepted, OrderList);
                customerRepo = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }
    }
}

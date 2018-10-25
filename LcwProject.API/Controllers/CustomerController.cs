using LcwProject.API.DTOs;
using LcwProject.API.Models;
using LcwProject.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LcwProject.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        [HttpPost]
        [Route("customers/")]
        public HttpResponseMessage PostCustomer(CustomerDTO customerDTO)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.PostCustomer(customerDTO);

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
        [Route("customers/{id}")]
        public HttpResponseMessage GetCustomer(int id)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerRepository customerRepo = new CustomerRepository();
                var customer = customerRepo.GetCustomer(id);

                message = Request.CreateResponse(HttpStatusCode.Accepted, customer);
                customerRepo = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }

        [HttpPut]
        [Route("customers/{id}")]
        public HttpResponseMessage PutCustomer(int id, CustomerDTO customerDTO)
        {
            HttpResponseMessage message = null;
            try
            {
                customerDTO.CustomerId = id;

                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.PostCustomer(customerDTO);

                message = Request.CreateResponse(HttpStatusCode.Accepted, "Güncelleme İşlem Tamamlandı");

                customerRepo = null;
            }
            catch (Exception ex)
            {
                message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return message;
        }

        [HttpDelete]
        [Route("customers/{id}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerRepository customerRepo = new CustomerRepository();
                customerRepo.DeleteCustomer(id);

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
        [Route("GetAllCustomers")]
        public HttpResponseMessage GetAllCustomers()
        {
            HttpResponseMessage message = null;
            try
            {
                CustomerRepository customerRepo = new CustomerRepository();
                var customerList = customerRepo.GetAllCustomers();

                message = Request.CreateResponse(HttpStatusCode.Accepted, customerList);
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

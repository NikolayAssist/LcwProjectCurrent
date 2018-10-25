using LcwProject.Core.Models;
using LcwProject.Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LcwProject.Core.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Session["OrderBoxList"] = null;

            ViewBag.Customers = CustomerRepository.GetCustomerList();
            ViewBag.Products = ProductRepository.GetProductList();
            ViewBag.message = "";
            var OrderViewDataModel = new CustomerOrderHeaderDTO()
            {
                CustomerId = 1,
                OrderId = 0,
                OrderRow = new CustomerOrderDTO()
                {
                    //ProductId = 1,
                    Barcode = string.Empty,
                    Amount = 1,
                    Price = 0,
                    GUID = string.Empty
                }
            };

            return View(OrderViewDataModel);

        }
        [HttpGet]
        public ActionResult GetIndex(int id)
        {
            ViewBag.Customers = CustomerRepository.GetCustomerList();
            ViewBag.Products = ProductRepository.GetProductList();

            var OrderViewDataModel = CustomerOrderRepository.GetOrder(id);

            List<OrderBox> BoxList = new List<OrderBox>();

            foreach (var item in OrderViewDataModel.CustomerOrder)
            {
                BoxList.Add(new OrderBox()
                {
                    Amount = item.Amount,
                    Barcode = item.Barcode,
                    Description = item.Description,
                    GUID = item.GUID,
                    Price = Convert.ToDecimal(item.Price),
                    ProductId = item.ProductId,
                    CustomerOrderId = item.CustomerOrderId,
                    ProductName = item.ProductName
                });
            }
            Session["OrderBoxList"] = BoxList;

            return View("Index", OrderViewDataModel);

        }
        [HttpPost]
        public PartialViewResult PostOrderBox(OrderBox Order)
        {
            if (Order.GUID == null)
                Order.GUID = Guid.NewGuid().ToString();

            List<OrderBox> OrderBoxList = Session["OrderBoxList"] as List<OrderBox>;

            if (OrderBoxList == null)
            {
                OrderBoxList = new List<OrderBox>();
                OrderBoxList.Add(Order);
            }
            else
            {
                OrderBoxList.Add(Order);
            }
            Session["OrderBoxList"] = OrderBoxList;

            return PartialView("_OrderBoxList");
        }
        [HttpGet]
        public PartialViewResult GetOrderBox(int id)
        {
            List<OrderBox> OrderBoxList = Session["OrderBoxList"] as List<OrderBox>;

            if (OrderBoxList != null)
            {
                var data = OrderBoxList.Where(a => a.CustomerOrderId == id).FirstOrDefault();
                OrderBoxList.Remove(data);
            }
            Session["OrderBoxList"] = OrderBoxList;
            return PartialView("_OrderBoxList");
        }
        [HttpGet]
        public JsonResult GetOrderBoxById(int id)
        {
            List<OrderBox> OrderBoxList = Session["OrderBoxList"] as List<OrderBox>;
            OrderBox OrderBox_ = null;
            if (OrderBoxList != null)
            {
                OrderBox_ = OrderBoxList.Where(a => a.CustomerOrderId == id).FirstOrDefault();
            }
            return Json(OrderBox_, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult EditOrderBoxByGuid(OrderBox Order)
        {
            List<OrderBox> OrderBoxList = Session["OrderBoxList"] as List<OrderBox>;
            OrderBox OrderBox_ = null;
            if (OrderBoxList != null)
            {
                OrderBox_ = OrderBoxList.Where(a => a.GUID == Order.GUID).FirstOrDefault();

                if (OrderBox_ != null)
                {
                    OrderBox_.Amount = Order.Amount;
                    OrderBox_.Barcode = Order.Barcode;
                    OrderBox_.ProductId = Order.ProductId;
                    OrderBox_.Description = Order.Description;
                    OrderBox_.Price = Order.Price;
                    OrderBox_.ProductName = Order.ProductName;
                }
            }
            Session["OrderBoxList"] = OrderBoxList;
            return PartialView("_OrderBoxList");
        }
        [HttpPost]
        public RedirectToRouteResult PostOrder(CustomerOrderHeaderDTO Order)
        {
            Order.Date_ = DateTime.Now;

            List<CustomerOrderDTO> list = new List<CustomerOrderDTO>();

            foreach (var item in Session["OrderBoxList"] as List<OrderBox>)
            {
                list.Add(new CustomerOrderDTO()
                {
                    Amount = item.Amount,
                    Barcode = item.Barcode,
                    Description = item.Description,
                    ProductId = item.ProductId,
                    GUID = item.GUID,
                    Price = Convert.ToDouble(item.Price),
                    CustomerOrderId = item.CustomerOrderId,
                    ProductName = item.ProductName
                });
            }
            Order.CustomerOrder = list;
            Order.OrderPrice = list.Sum(a => a.Price) * list.Sum(a => a.Amount);

            Repositories.CustomerOrderRepository.CustomerOrderPost(Order);
            ViewBag.message = "Siparişiniz Eklendi";
            return RedirectToAction("GetAllorders");
        }
        [HttpGet]
        public ActionResult GetAllorders()
        {
            var CustomerOrderRepo = Repositories.CustomerOrderRepository.GetAllOrderList();
            return View(CustomerOrderRepo);
        }
    }
}
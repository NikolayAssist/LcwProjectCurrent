using LcwProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LcwProject.Core.Controllers
{
    public class productController : Controller
    {
        // GET: product
        public ActionResult Index()
        {
            var list = Repositories.ProductRepository.GetProductList();
            return View(list);
        }
        public ActionResult GetIndex(int id)
        {
            var product = Repositories.ProductRepository.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(ProductDTO product)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return View("GetIndex", product);
                }
                Repositories.ProductRepository.PostProduct(product);
                ViewBag.message = "İşlem Tamamlandı";
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message.ToString();
            }
            return Redirect("index");
        }
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                Repositories.ProductRepository.DeleteProduct(id);
                ViewBag.message = "İşlem Tamamlandı";
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message.ToString();
            }
            return Redirect("/product/index");
        }
        public ActionResult GetIndexNew()
        {
            var ProductDataModel = new ProductDTO() { Barcode = string.Empty, Description = string.Empty, Price = 0, ProductId = 0, Quantity = 0 };
            return View("GetIndex", ProductDataModel);
        }
    }
}
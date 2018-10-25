using LcwProject.API.DTOs;
using LcwProject.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.Repositories
{
    public class ProductRepository
    {
        public void PostProduct(ProductDTO productDTO)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                var Data = (from d in db.Product
                            where d.ProductId == productDTO.ProductId
                            select d).FirstOrDefault();

                if (Data != null)
                {
                    Data.Barcode = productDTO.Barcode;
                    Data.Description = productDTO.Description;
                    Data.Quantity = productDTO.Quantity;
                    Data.Price = productDTO.Price;
                }
                else
                {
                    db.Product.Add(new Product()
                    {
                        Barcode = productDTO.Barcode,
                        Description = productDTO.Description,
                        Price = productDTO.Price,
                        Quantity = productDTO.Quantity
                    });
                }

                db.SaveChanges();

            }
        }
        public void DeleteProduct(int id)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                var data = db.Product.Where(a => a.ProductId == id).FirstOrDefault();
                if (data != null)
                {
                    db.Product.Remove(data);
                    db.SaveChanges();
                }
            }
        }
        public List<ProductDTO> GetProductList()
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                return (from d in db.Product
                        select new ProductDTO()
                        {
                            Barcode = d.Barcode,
                            Description = d.Description,
                            Price = d.Price,
                            ProductId = d.ProductId,
                            Quantity = d.Quantity
                        }).ToList();
            }
        }
        public ProductDTO GetProductById(int id)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                return (from d in db.Product
                        where d.ProductId == id
                        select new ProductDTO()
                        {
                            Barcode = d.Barcode,
                            Description = d.Description,
                            Price = d.Price,
                            ProductId = d.ProductId,
                            Quantity = d.Quantity
                        }).FirstOrDefault();
            }
        }
    }
}
using LcwProject.API.DTOs;
using LcwProject.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.Repositories
{
    public class CustomerOrderRepository
    {
        public void PostOrder(CustomerOrderHeaderDTO customerOrderDTO)
        {
            CustomerOrderHeader Order = null;

            using (LcwDBEntities db = new LcwDBEntities())
            {
                var Data = (from d in db.CustomerOrderHeader
                            where d.OrderId == customerOrderDTO.OrderId
                            select d).FirstOrDefault();

                int OrderId = 0;

                if (Data != null)
                {
                    Data.OrderAdress = customerOrderDTO.OrderAdress;
                    Data.Date_ = customerOrderDTO.Date_;
                    Data.OrderPrice = customerOrderDTO.OrderPrice;
                    Data.CustomerId = customerOrderDTO.CustomerId;
                    OrderId = customerOrderDTO.OrderId;
                    db.SaveChanges();
                }
                else
                {
                    Order = new CustomerOrderHeader()
                    {
                        CustomerId = customerOrderDTO.CustomerId,
                        OrderAdress = customerOrderDTO.OrderAdress,
                        OrderPrice = customerOrderDTO.OrderPrice,
                        Date_ = customerOrderDTO.Date_
                    };

                    db.CustomerOrderHeader.Add(Order);
                    db.SaveChanges();
                    OrderId = Order.OrderId;
                }

                
                
                

                var DBList = (from r in db.CustomerOrder
                              where r.OrderId == customerOrderDTO.OrderId
                              select r).ToList();

                var newRows = customerOrderDTO.CustomerOrder;


                foreach (var iRow in newRows)
                    iRow.OrderId = OrderId;
               
                // Güncelleme varmı ?
                foreach (var iNewRow in newRows)
                {
                    var data = DBList.Where(a => a.CustomerOrderId == iNewRow.CustomerOrderId).FirstOrDefault();
                    if (data != null)
                    {
                        data.Amount = iNewRow.Amount;
                        data.ProductId = iNewRow.ProductId;
                        data.Price = iNewRow.Price;
                        data.Description = iNewRow.Description;
                        data.Barcode = iNewRow.Barcode;
                        db.SaveChanges();
                    }
                }
                // Eklenen var mı ?
                foreach (var iNewRow in newRows)
                {
                    var data = DBList.Where(a => a.CustomerOrderId == iNewRow.CustomerOrderId).FirstOrDefault();
                    if (data == null)
                    {
                        db.CustomerOrder.Add(new CustomerOrder()
                        {
                            Amount = iNewRow.Amount,
                            Price = iNewRow.Price,
                            ProductId = iNewRow.ProductId,
                            OrderId = iNewRow.OrderId,
                            Description = iNewRow.Description,
                            Barcode = iNewRow.Barcode,
                            Guid = iNewRow.Guid
                        });
                        db.SaveChanges();
                    }
                }

                // Silinen var mı ?

                foreach (var iDBRow in DBList)
                {
                    var data = newRows.Where(a => a.CustomerOrderId == iDBRow.CustomerOrderId).FirstOrDefault();
                    if (data == null)
                    {
                        db.CustomerOrder.Remove(iDBRow);
                        db.SaveChanges();
                    }
                }

            }
        }
        public void DeleteOrder(int OrderId)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                var Rows = db.CustomerOrder.Where(a => a.OrderId == OrderId).ToList();
                db.CustomerOrder.RemoveRange(Rows);

                db.SaveChanges();

                var data = db.CustomerOrderHeader.Where(a => a.OrderId == OrderId).FirstOrDefault();
                if (data != null)
                {
                    db.CustomerOrderHeader.Remove(data);
                    db.SaveChanges();
                }
               
            }
        }

        public CustomerOrderHeaderDTO GetOrder(int OrderId)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                var order = (from c in db.CustomerOrderHeader
                             where c.OrderId == OrderId
                             select new CustomerOrderHeaderDTO
                             {
                                 CustomerId = c.CustomerId,
                                 OrderId = c.OrderId,
                                 OrderAdress = c.OrderAdress,
                                 CustomerOrder = (from k in db.CustomerOrder
                                                  where k.OrderId == OrderId
                                                  select new CustomerOrderDTO
                                                  {
                                                      Amount = k.Amount,
                                                      Description = k.Description,
                                                      Barcode = k.Barcode,
                                                      Price = k.Price,
                                                      ProductId = k.ProductId,
                                                      OrderId = k.OrderId,
                                                      CustomerOrderId = k.CustomerOrderId,
                                                      Guid = k.Guid,
                                                      ProductName = (from p in db.Product
                                                                    where p.ProductId == k.ProductId
                                                                    select p).FirstOrDefault().Description
                                                  }).ToList()
                             }).FirstOrDefault();

                return order;
            }
        }
        public List<CustomerOrderHeaderDTO> GetAllOrders()
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                return (from c in db.CustomerOrderHeader
                        select new CustomerOrderHeaderDTO
                        {
                            OrderId = c.OrderId,
                            Date_ = c.Date_,
                            OrderAdress = c.OrderAdress,
                            OrderPrice = c.OrderPrice,
                            CustomerName = c.Customer.CustomerName
                        }).ToList();
            }
        }

    }
}
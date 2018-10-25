using LcwProject.API.DTOs;
using LcwProject.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LcwProject.API.Repositories
{
    public class CustomerRepository
    {
        public void PostCustomer(CustomerDTO customerDTO)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                var Data = (from d in db.Customer
                            where d.CustomerId == customerDTO.CustomerId
                            select d).FirstOrDefault();

                if (Data != null)
                {
                    Data.CustomerAdress = customerDTO.CustomerAdress;
                    Data.CustomerName = customerDTO.CustomerName;
                }
                else
                {
                    db.Customer.Add(new Customer()
                    {
                        CustomerAdress = customerDTO.CustomerAdress,
                        CustomerName = customerDTO.CustomerName
                    });
                }

                db.SaveChanges();

            }
        }

        public CustomerDTO GetCustomer(int id)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                return (from c in db.Customer
                        where c.CustomerId == id
                        select new CustomerDTO
                        {
                            CustomerAdress = c.CustomerAdress,
                            CustomerId = c.CustomerId,
                            CustomerName = c.CustomerName
                        }).FirstOrDefault();
            }
        }
        public void DeleteCustomer(int id)
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                var data = db.Customer.Where(a => a.CustomerId == id).FirstOrDefault();
                if (data != null)
                {
                    db.Customer.Remove(data);
                    db.SaveChanges();
                }
            }
        }
        public List<CustomerDTO> GetAllCustomers()
        {
            using (LcwDBEntities db = new LcwDBEntities())
            {
                return (from d in db.Customer
                        select new CustomerDTO
                        {
                            CustomerAdress = d.CustomerAdress,
                            CustomerId = d.CustomerId,
                            CustomerName = d.CustomerName
                        }).ToList();
                
            }
        }

    }
}
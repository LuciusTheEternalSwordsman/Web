using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class CustomerService : IRepository<Customer>
    {
        private CustomerContext db;
        public CustomerService(CustomerContext context)
        {
            this.db = context;
        }
        public IEnumerable<Customer> GetCustomersList()
        {
            return db.Customers;
        }
        public Customer GetCustomer(int id)
        {           
            return db.Customers.Find(id);
            
        }
        public void Create(Customer customer)
        {
            db.Customers.Add(customer);
        }
        public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null) db.Customers.Remove(customer); 
        }
        public void Save()
        {
            db.SaveChanges();
        }       
        
    }
}

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
        public int Create(Customer customer)
        {
            if (customer.Password == null       || customer.Surname == null
                || customer.Address1 == null    || customer.Postcode == null
                || customer.Town == null        || customer.EmailAdress == null
                || customer.Datetime == null    || customer.UpdatedBy == null)
                return -1;
            else { db.Customers.Add(customer); return 1; }
        }
        public void Update(int id, string address1, string postcode, string town, string phonenum1, string email, DateTime datechg)
        {
            Customer customer = db.Customers.Find(id);
            if (address1 != null)
                customer.Address1 = address1;
            if(postcode !=null)
                customer.Postcode = postcode;
            if(town!=null) 
                customer.Town = town;
            if (phonenum1 != null)
                customer.PhoneNumber1 = phonenum1;
            if (email != null)
                customer.EmailAdress = email;
            if (datechg != null)
                customer.Datetime = datechg;
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
/*
 public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }
 */
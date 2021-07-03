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
            //using (CustomerContext context = new CustomerContext())        {           }
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
        public void Update(int id, string address1, string postcode, string town, string phonenum1, string email, DateTime datechg)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                if (address1 != null)
                    customer.Address1 = address1;
                if (postcode != null)
                    customer.Postcode = postcode;
                if (town != null)
                    customer.Town = town;
                if (phonenum1 != null)
                    customer.PhoneNumber1 = phonenum1;
                if (email != null)
                    customer.EmailAdress = email;
                DateTime dt = new DateTime();
                if (DateTime.TryParse(datechg.ToString(),out dt)==false)
                    customer.Datetime = datechg;
                db.Entry(customer).State = EntityState.Modified;
            }
            catch(Exception ex) {  }
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
        
        public void Exc(string ex)
        {
            
        }
    }
}
/*
 public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }
 */
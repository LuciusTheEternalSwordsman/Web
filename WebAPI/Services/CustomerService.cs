using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class CustomerService : IRepository<Customer,UpdateCustomer>
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
        public IAsyncEnumerable<Customer> GetCustomersAsyncList()
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
        //int id, string address1, string postcode, string town, string phonenum1, string email, DateTime datechg
        public void Update(UpdateCustomer ucustomer)
        {
            try
            {
                Customer customer = db.Customers.Find(ucustomer.CustomerNumber);
                if (ucustomer.Address1 != null)
                    customer.Address1 = ucustomer.Address1;
                if (ucustomer.Postcode != null)
                    customer.Postcode = ucustomer.Postcode;
                if (ucustomer.Town != null)
                    customer.Town = ucustomer.Town;
                if (ucustomer.PhoneNumber1 != null)
                    customer.PhoneNumber1 = ucustomer.PhoneNumber1;
                if (ucustomer.EmailAddress != null)
                    customer.EmailAdress = ucustomer.EmailAddress;
                DateTime dt = new DateTime();
                if (DateTime.TryParse(ucustomer.DateChanged.ToString(),out dt)==false)
                    customer.Datetime = ucustomer.DateChanged;
                db.Entry(customer).State = EntityState.Modified;
            }
            catch(Exception ex) { throw ex; }
        }
        public void Delete(int id)
        {
            var customer = db.Customers.FirstOrDefault(p => p.CustomerNumber == id);
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
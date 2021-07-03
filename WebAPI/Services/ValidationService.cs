using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ValidationService : IValidator<Customer>
    {
        public bool IsValid(Customer customer)
        {
            if (customer.Password == null || customer.Surname == null
                || customer.Address1 == null || customer.Postcode == null
                || customer.Town == null || customer.EmailAdress == null
                || customer.Datetime == null || customer.UpdatedBy == null) return false;
            else  return true;
        }
        public bool IsValid(int id)
        {            
            if (id <= 0) return false;
            else return true;
        }        
    }
}


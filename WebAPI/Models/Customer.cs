using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Customer
    {
        
        public int Id { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string FirstNames { get; set; }
        public string Address1 { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        public string PhoneNumber1 { get; set; }
        public string EmailAdress { get; set; }
        public DateTime Datetime { get; set; }
        public string UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    public class Customer
    {

        [Key]
        public int CustomerNumber { get; set; }
        [MaxLength(50)] [Required]
        public string Password { get; set; }
        [MaxLength(50)] [Required]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string FirstNames { get; set; }
        [MaxLength(100)] [Required]
        public string Address1 { get; set; }
        [MaxLength(10)] [Required]
        public string Postcode { get; set; }
        [MaxLength(50)] [Required]
        public string Town { get; set; }
        [MaxLength(20)]
        public string PhoneNumber1 { get; set; }
        [MaxLength(100)] [Required]
        public string EmailAdress { get; set; }
        public DateTime Datetime { get; set; }
        [MaxLength(255)] [Required]
        public string UpdatedBy { get; set; }
        public Customer(string password, string surname, string firstNames, string address1, string postcode, string town, string phoneNumber1, string emailAdress, DateTime datetime, string updatedBy) 
        {
            Password = password; Surname = surname; FirstNames = firstNames;
            Address1 = address1; Postcode = postcode; Town = town;
            PhoneNumber1 = phoneNumber1; EmailAdress = emailAdress; Datetime = datetime;
            UpdatedBy = updatedBy;
        }
            
    }
}

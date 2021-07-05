using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UpdateCustomer
    {
        [Key]
        public int CustomerNumber { get; set; }
        [MaxLength(100)]
        public string Address1 { get; set; }
        [MaxLength(10)]
        public string Postcode { get; set; }
        [MaxLength(50)]
        public string Town { get; set; }
        [MaxLength(20)]
        public string PhoneNumber1 { get; set; }
        [MaxLength(100)]
        public string EmailAddress { get; set; }
        public DateTime DateChanged { get; set; }

    }
}

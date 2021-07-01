using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<int,int> ValidationMessage { get; set; }
        public int CustomerNumber { get; set; }
    }
}

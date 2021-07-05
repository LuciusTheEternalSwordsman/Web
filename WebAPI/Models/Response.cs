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
        public List<ValidMess> ValidationMessage { get; set; }
        public int CustomerNumber { get; set; }
    }
    public class ValidMess
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}

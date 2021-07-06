using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ResponseService : IRespons<ValidMess>
    {
        public Object FResponse(bool success,string ex, List<ValidMess> valid,int cus)
        {
            if (success) 
            {                
                var result = new { Success = success, CustomerNumber = cus};
                return result; 
            }
            else return new Response()
            {
                Success = success,
                ErrorMessage = ex,
                ValidationMessage = valid,
                CustomerNumber = cus
            }; ;
        }        
    }
}

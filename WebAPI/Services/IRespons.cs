using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    interface IRespons<T>
        where T:class
        
    {
        Object FResponse(bool success,string ex,List<T> valid,int cus);
        
    }
}

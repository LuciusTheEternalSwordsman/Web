using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    interface IValidator<T>
        where T: class
    {
        bool IsValid(T item);
        bool IsValid(int id);
    }
}

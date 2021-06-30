using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    interface IRepository<T>
        where T:class
    {
        IEnumerable<T> GetCustomersList();
        T GetCustomer(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}

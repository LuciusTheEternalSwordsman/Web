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
        void Update(int id, string address1, string postcode, string town, string phonenum1, string email, DateTime datechg);
        void Delete(int id);
        void Save();
    }
}

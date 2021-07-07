using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    interface IRepository<T,Y>
        where T: class
        where Y: class
    {
        IEnumerable<T> GetCustomersList();
        IAsyncEnumerable<T> GetCustomersAsyncList();
        T GetCustomer(int id);
        void Create(T item);
        void Update(Y customer);
        void Delete(int id);
        void Save();
    }
}

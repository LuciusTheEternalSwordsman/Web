using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerContext db;
        IRepository<Customer> db1;
        public CustomerController(CustomerContext context)
        {
            db = context;
            db1 = new CustomerService(context);
        }
        //GET all action
        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            var customer = db1.GetCustomersList().OrderBy(u=>u.Id).ToList();
            // return customer;
            return customer;
        }

        //public ActionResult<List<Customer>> GetAll() => CustomerService.GetAll();

        //GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = db1.GetCustomer(id);
            if (customer == null) return NotFound();
            return customer;
        }
        //POST action
        [Route("CreateCustomer")]
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db1.Create(customer);
                db1.Save();
                return RedirectToAction("GetAll");
            }
            //db.Customers.Add(customer);
            //db.SaveChangesAsync();
            return RedirectToAction("GetAll"); ;
            //CustomerService.Add(customer);
            //return CreatedAtAction(nameof(Create), new { id = customer.Id }, customer);
        }
        //PUT action
        [Route("UpdateCustomer")]
        [HttpPost]
        public IActionResult Update(int id, Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db1.Update(customer);
                    db1.Save();
                    return RedirectToAction("GetAll");
                }
                return NotFound();
            }
            catch { return NotFound(); ; }
            /*if (id != customer.Id) return BadRequest();
            var existingCustomer = db1.GetCustomer(id);
            if (existingCustomer is null) return NotFound();
            db1.Update(customer);
            return NoContent();*/
        }
        //DELETE action
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var customer = db1.GetCustomer(id);
            if (customer is null) return NotFound();
            db1.Delete(id);
            return NoContent();
        }
        
    }
}

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
        public bool success=false;
       
        IRepository<Customer> db1;
        public CustomerController(CustomerContext context)
        {            
            db1 = new CustomerService(context);
        }
        //GET all action
        [HttpGet]
        public ActionResult<List<Customer>> GetAll()=> db1.GetCustomersList().OrderBy(u => u.CustomerNumber).ToList();
                
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
        public IActionResult Create(string password, string surname,string firstName,string address1,string postcode,string town,string phoneNumber1,string email,DateTime dtChanged,string updatedBy)
        {
            try
            {
                int i = 0;
                Customer customer = new Customer(password,surname,firstName,address1,postcode,town,phoneNumber1,email,dtChanged,updatedBy);
                if (ModelState.IsValid)
                {

                    i = db1.Create(customer);
                    if (i == -1) return BadRequest();
                    else db1.Save();                    
                }
                return Ok("Customer Number: "+customer.CustomerNumber.ToString());
                
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
        //PUT action
        [Route("UpdateCustomer")]
        [HttpPost]
        public IActionResult Update(int id, string address1, string postcode, string town, string phonenum1, string email, DateTime datechg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db1.Update(id,address1,postcode,town,phonenum1,email,datechg);
                    db1.Save();                   
                }
                return Ok("Customer number " + id.ToString()); ;                
            }
            catch(Exception ex) { return BadRequest("Code: "+BadRequest().StatusCode.ToString()+"\nError text: "+ex.Message+"\n Customer number to update: "+id.ToString()); }  
            //catch(Exception ex) { return "Error: "+ex.Message; }
        }
        //DELETE action
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = db1.GetCustomer(id);
                if (customer is null) return NotFound();
                db1.Delete(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
/*
  public IActionResult Update(int id, Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db1.Update(customer);
                    db1.Save();
                   // return Ok("Customer number "+customer.CustomerNumber.ToString());
                   //return RedirectToAction("GetAll");
                }
                return Ok("Customer number " + customer.CustomerNumber.ToString());
                //else throw new Exception("Not valid CustomerNumber "+customer.CustomerNumber.ToString());
            }
            catch(Exception ex) { return BadRequest("Error: "+ex.Message+"\n Customer number to update: "+customer.CustomerNumber.ToString()); }  
            //catch(Exception ex) { return "Error: "+ex.Message; }
        }
public IActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db1.Create(customer);
                    db1.Save();
                    success = true;
                }
                else throw new Exception("Success failure");
                if (success)
                    return Ok("Customer Number: "+customer.CustomerNumber.ToString());
                else return BadRequest();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
 */
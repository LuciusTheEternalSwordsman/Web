using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public bool success=false;
       
        IRepository<Customer,UpdateCustomer> db1;
        IValidator<Customer> vs;
        IRespons<ValidMess> res;
        
        public CustomerController(CustomerContext context)
        {            
            db1 = new CustomerService(context);
            vs = new ValidationService();
            res = new ResponseService();
        }
        //GET all action
        [HttpGet]
        [Consumes("application/json")]
        public ActionResult<List<Customer>> GetAll()=> db1.GetCustomersList().OrderBy(u => u.CustomerNumber).ToList();
        
       
        //GET by Id action
        /*[HttpGet("{id}")]
        [Consumes("application/json")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = db1.GetCustomer(id);
            if (customer == null) return NotFound();
            return customer;
        }*/
        //POST action
        [HttpPost("CreateCustomer")]
        [Consumes("application/json")]
        public IActionResult Create(Customer customer)
        {
            try
            {                    
                    if (vs.IsValid(customer))
                    {
                    List<ValidMess> _valid = new List<ValidMess>();
                    _valid.Add(new ValidMess()
                    {
                        Code = Ok().StatusCode,
                        Message = Ok().ToString()
                    });
                    db1.Create(customer);
                    db1.Save();
                    return Ok(res.FResponse(true,"Ok",_valid,customer.CustomerNumber));
                    }
                    else throw new Exception("Null values");                          
            }
            catch(Exception ex) 
            {                
                List<ValidMess> _valid = new List<ValidMess>();
                _valid.Add(new ValidMess()
                {
                    Code = BadRequest().StatusCode,
                    Message = BadRequest().ToString()
                });                
                return BadRequest(res.FResponse(false,ex.Message,_valid,customer.CustomerNumber));
            }
        }
        //PUT action
        [HttpPost("UpdateCustomer")]
        [Consumes("application/json")]
        public IActionResult Update(UpdateCustomer ucustomer)
        {
            try
            {
                if (vs.IsValid(ucustomer.CustomerNumber))
                {
                    db1.Update(ucustomer);
                    db1.Save();                   
                    return Ok(res.FResponse(true,"",null,ucustomer.CustomerNumber));
                }
                else throw new Exception("Incorrect values");
                               
            }
            catch(Exception ex) 
            {               
                List<ValidMess> _valid = new List<ValidMess>();
                _valid.Add(new ValidMess() 
                { 
                    Code = BadRequest().StatusCode,
                    Message = BadRequest().ToString()
                });  
                return BadRequest(res.FResponse(false,ex.Message,_valid,ucustomer.CustomerNumber));                
            }  
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
                db1.Save();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }       
       
    }
}
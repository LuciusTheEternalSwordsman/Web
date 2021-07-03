﻿using Microsoft.AspNetCore.Http;
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
        IValidator<Customer> vs;
        Response response;
        public CustomerController(CustomerContext context)
        {            
            db1 = new CustomerService(context);
            vs = new ValidationService();
            response = new Response() { Success = true, ErrorMessage = "", ValidationMessage = new Dictionary<int, string>(), CustomerNumber = 0 };
        }
        //GET all action
        [HttpGet]
        [Consumes("application/json")]
        public ActionResult<List<Customer>> GetAll()=> db1.GetCustomersList().OrderBy(u => u.CustomerNumber).ToList();
        
        [HttpGet("async")]
        public async IAsyncEnumerable<Customer> GetCustomers()
        {
            var customers = db1.GetCustomersAsyncList();
            await foreach(var customer in customers)
            {
                yield return customer;
            }
        }
        //GET by Id action
        [HttpGet("{id}")]
        [Consumes("application/json")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = db1.GetCustomer(id);
            if (customer == null) return NotFound();
            return customer;
        }
        //POST action        
        [HttpPost("CreateCustomer")]
        [Consumes("application/json")]
        public IActionResult Create(string password, string surname,string firstName,string address1,string postcode,string town,string phoneNumber1,string email,DateTime dtChanged,string updatedBy)
        {
            try
            {                               
                    Customer customer = new Customer(password, surname, firstName, address1, postcode, town, phoneNumber1, email, dtChanged, updatedBy);
                    if (vs.IsValid(customer))
                    {
                        db1.Create(customer);
                        db1.Save();
                        return Success(true, customer.CustomerNumber);
                    }
                    else throw new Exception("Null values");                          
            }
            catch(Exception ex) { return GetError(ex); }
        }
        //PUT action        
        [HttpPost("UpdateCustomer")]
        [Consumes("application/json")]
        public IActionResult Update(int id, string address1, string postcode, string town, string phonenum1, string email, DateTime datechg)
        {
            try
            {
                if (vs.IsValid(id))
                {
                    db1.Update(id, address1, postcode, town, phonenum1, email, datechg);
                    db1.Save();                   
                    return Success(true,id);
                }
                else throw new Exception("id can not be <1");
                               
            }
            catch(Exception ex) 
            {               
                return GetError(ex);
                //return BadRequest("Code: "+BadRequest().StatusCode.ToString()+"\nError text: "+ex.Message+"\n Customer number to update: "+id.ToString()); 
            }  
            //catch(Exception ex) { return "Error: "+ex.Message; }
        }
        //DELETE action
        [HttpDelete]
        [Consumes("application/json")]
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
        [HttpGet("error")]
        public IActionResult GetError(Exception ex)
        {
            string result = "";
            response.Success = false;
            response.ErrorMessage = ex.Message;
            response.ValidationMessage.Add(BadRequest().StatusCode, BadRequest().ToString());
            foreach (var item in response.ValidationMessage)
            {
                result += "Code: " + item.Key + " Name: " + item.Value;
            }
            Object[] ArrayOfObjects = new Object[] { "Success "+response.Success.ToString(), response.ErrorMessage, result };
            return BadRequest(ArrayOfObjects);
        }
        [HttpGet("Success")]
        public IActionResult Success(bool success, int id)
        {
            Object[] SuccessObject = new Object[] { "Success "+success.ToString(), "Customer number: "+id };
            return Ok(SuccessObject);
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
using D2P0JX_SG1_21_22_2.Logic.Interfaces;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerLogic customerLogic;


        public CustomerController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;

        }


        // GET: api/Customer/GetAll
        [HttpGet]
        [ActionName("GetAll")] //EZEK MÉG KELLENEK
        public IEnumerable<Customer> Get()
        {
            return customerLogic.ReadAll();
        }

        // GET api/Customer/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return customerLogic.Read(id);
        }

        // POST api/Customer/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(Customer customer)
        {
            var result = new ApiResult(true);

            try
            {
                customerLogic.Create(customer);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }

            return result;
        }

        // PUT api/Customer/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(Customer customer)
        {
            var result = new ApiResult(true);

            try
            {
                customerLogic.Update(customer);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }

            return result;
        }

        // DELETE api/Customer/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
                customerLogic.Delete(id);
            }
            catch (Exception)
            {

                result.IsSuccess = false;
            }

            return result;
        }
    }
}

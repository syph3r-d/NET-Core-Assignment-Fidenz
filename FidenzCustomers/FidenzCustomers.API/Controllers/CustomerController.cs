﻿using Asp.Versioning;
using AutoMapper;
using FidenzCustomers.API.Common.Utility;
using FidenzCustomers.Application.Common.Interfaces;
using FidenzCustomers.Application.Common.Services;
using FidenzCustomers.Application.DTOs;
using FidenzCustomers.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidenzCustomers.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICustomerManager _customerManager;

        public CustomersController(ICustomerManager customerManager, IMapper mapper)
        {
            _mapper = mapper;
            _customerManager = customerManager;
        }

        [Authorize(policy: SD.Role_Admin)]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _customerManager.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CustomerUpdateDto customer)
        {
            try
            {

                var oldCustomer = _customerManager.GetCustomerById(id);

                if (oldCustomer == null)
                {
                    return NotFound("Invaild Id");
                }
                if (id != customer.CustomerId)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    //map oldcustomer to customer
                    //_mapper.Map<CustomerUpdateDto,Customer>(customer, oldCustomer);
                    _customerManager.UpdateCustomer(customer);
                    return Ok();

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //api/Customers/Distance/5?latitude=&longitude=
        [HttpGet("[action]/{id}")]
        public IActionResult Distance(string id, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                if (latitude == 0 || longitude == 0)
                {
                    return BadRequest("Invaild Latitude or Longitude");
                }
                var customer = _customerManager.GetCustomerById(id);

                if (customer == null)
                {
                    return NotFound("Invaild Id");
                }

                return Ok(DistanceCalculator.CalculateDistance(customer.Latitude, customer.Longitude, latitude, longitude));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        [HttpGet("[action]/")]
        public IActionResult Search([FromQuery] string q)
        {
            try
            {
                var result = _customerManager.SearchCustomers(q);
                if (result == null)
                {
                    return NotFound("No customers found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult GroupByZipCode()
        {
            try
            {

                var result = _customerManager.GetAllCustomers();
                return Ok(result.GroupBy(c => c.Address.Zip).Select(grp => new { ZipCode = grp.Key, Customers = grp.ToList() }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


    }
}

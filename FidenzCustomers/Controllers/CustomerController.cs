using FidenzCustomers.Common.Interfaces;
using FidenzCustomers.Common.Services;
using FidenzCustomers.Common.Uitility;
using FidenzCustomers.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FidenzCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfwork;


        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }

        [Authorize(policy:SD.Role_Admin)]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _unitOfwork.Customer.GetAll(null, "Address");

            return Ok(customers);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CustomerUpdateDto customer)
        {
            //var oldCustomer = await _dbContext.Customer.FindAsync(id);
            var oldCustomer = _unitOfwork.Customer.Get(c => c.CustomerId == id, "Address");

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
                oldCustomer.Index = customer.Index;
                oldCustomer.Age = customer.Age;
                oldCustomer.EyeColor = customer.EyeColor;
                oldCustomer.Gender = customer.Gender;
                oldCustomer.Company = customer.Company;
                oldCustomer.Email = customer.Email;
                oldCustomer.Phone = customer.Phone;
                oldCustomer.About = customer.About;
                oldCustomer.Registered = customer.Registered;
                oldCustomer.Latitude = customer.Latitude;
                oldCustomer.Longitude = customer.Longitude;
                oldCustomer.Tag = customer.Tag;
                oldCustomer.Address.Number = customer.Address.Number;
                oldCustomer.Address.Street = customer.Address.Street;
                oldCustomer.Address.City = customer.Address.City;
                oldCustomer.Address.State = customer.Address.State;
                oldCustomer.Address.Zip = customer.Address.Zip;

                _unitOfwork.Customer.Save();
                return Ok();

            }
            return BadRequest();
        }
        //api/Customers/Distance/5?latitude=&longitude=
        [HttpGet("[action]/{id}")]
        public IActionResult Distance(string id, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            var customer = _unitOfwork.Customer.Get(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound("Invaild Id");
            }

            return Ok(DistanceCalculator.CalculateDistance(customer.Latitude, customer.Longitude, latitude, longitude));

        }

        [HttpGet("[action]/")]
        public IActionResult Search([FromQuery] string q)
        {
            var result = _unitOfwork.Customer.GetAll(c => c.Name.Contains(q) || c.EyeColor.Contains(q) || c.Company.Contains(q) || c.Email.Contains(q));
            if (result == null)
            {
                return NotFound("Invaild Name");
            }
            return Ok(result);
        }

        [HttpGet("[action]")]
        public IActionResult GroupByZipCode()
        {
            var result = _unitOfwork.Customer.GetAll(null, "Address");
            if (result == null)
            {
                return NotFound("Invaild Name");
            }
            return Ok(result.GroupBy(c => c.Address.Zip).Select(grp => new { ZipCode = grp.Key, Customers = grp.ToList() }));
        }


    }
}

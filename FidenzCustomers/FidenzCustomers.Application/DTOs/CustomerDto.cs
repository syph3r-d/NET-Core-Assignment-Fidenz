using FidenzCustomers.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FidenzCustomers.Application.DTOs
{
    public class CustomerDto
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set;}
        public AddressDto Address { get; set; }
        public string Gender { get; set; }
        public string EyeColor { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class AddressDto
    {
        public string AddressId { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public required string State { get; set; }
        public int Zip { get; set; }
    }
}

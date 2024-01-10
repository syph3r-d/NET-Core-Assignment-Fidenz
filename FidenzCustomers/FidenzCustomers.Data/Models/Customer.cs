using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FidenzCustomers.Data.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonPropertyName("_id")]
        public required string CustomerId { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("eyeColor")]
        [Display(Name = "Eye Color")]
        public required String EyeColor { get; set; }
        [JsonPropertyName("name")]

        public required string Name { get; set; }
        [JsonPropertyName("gender")]

        public required string Gender { get; set; }
        [JsonPropertyName("company")]
        public required string Company { get; set; }
        [JsonPropertyName("email")]
        public required string Email { get; set; }
        [JsonPropertyName("phone")]
        public required string Phone { get; set; }
        [JsonPropertyName("address")]
        public virtual Address Address { get; set; }
        [JsonPropertyName("about")]
        public required string About { get; set; }
        [JsonPropertyName("registered")]
        public string Registered { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tag { get; set; }
    }

    public class Address
    {
        [ForeignKey("Customer")]
        public int AddressId { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public required string State { get; set; }
        [JsonPropertyName("zipcode")]
        public int Zip { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}

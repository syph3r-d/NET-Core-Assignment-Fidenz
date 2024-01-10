using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FidenzCustomers.Data.Models
{
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

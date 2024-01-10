using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

}

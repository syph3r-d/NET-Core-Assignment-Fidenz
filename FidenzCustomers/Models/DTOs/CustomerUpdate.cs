using System.Text.Json.Serialization;

namespace FidenzCustomers.Models.DTOs
{
    public class CustomerUpdateDto
    {
        [JsonPropertyName("_id")]
        public string CustomerId { get; set; }
        [JsonPropertyName("index")]

        public int Index { get; set; }
        [JsonPropertyName("age")]

        public int Age { get; set; }
        [JsonPropertyName("eyeColor")]

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
        public AddressUpdateDto Address { get; set; }
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
    public class AddressUpdateDto
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("zipcode")]
        public int Zip { get; set; }
    }
}

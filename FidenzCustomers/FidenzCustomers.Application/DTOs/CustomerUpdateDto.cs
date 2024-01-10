namespace FidenzCustomers.Application.DTOs
{
    public class CustomerUpdateDto
    {
        public string CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}

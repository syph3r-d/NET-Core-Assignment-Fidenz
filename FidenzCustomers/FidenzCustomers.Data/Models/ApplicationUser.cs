using Microsoft.AspNetCore.Identity;


namespace FidenzCustomers.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

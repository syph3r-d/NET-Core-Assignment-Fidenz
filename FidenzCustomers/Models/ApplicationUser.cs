using Microsoft.AspNetCore.Identity;

namespace FidenzCustomers.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

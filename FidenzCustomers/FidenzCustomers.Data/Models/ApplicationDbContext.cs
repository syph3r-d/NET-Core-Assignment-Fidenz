using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace FidenzCustomers.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var jsonData = File.ReadAllText("../FidenzCustomers.Data/UserData.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(jsonData);
            foreach (var customer in customers.Select((Value, Index) => new { Value, Index }))
            {
                modelBuilder.Entity<Customer>().HasData(new
                {
                    Id = customer.Index + 1,
                    customer.Value.CustomerId,
                    customer.Value.Index,
                    customer.Value.Age,
                    customer.Value.EyeColor,
                    customer.Value.Name,
                    customer.Value.Gender,
                    customer.Value.Company,
                    customer.Value.Email,
                    customer.Value.Phone,
                    customer.Value.About,
                    customer.Value.Registered,
                    customer.Value.Latitude,
                    customer.Value.Longitude,
                    customer.Value.Tag
                });

                modelBuilder.Entity<Address>().HasData(new
                {
                    AddressId = customer.Index + 1,
                    customer.Value.Address.Number,
                    customer.Value.Address.Street,
                    customer.Value.Address.City,
                    customer.Value.Address.State,
                    customer.Value.Address.Zip
                });
            }
        }
    }
}

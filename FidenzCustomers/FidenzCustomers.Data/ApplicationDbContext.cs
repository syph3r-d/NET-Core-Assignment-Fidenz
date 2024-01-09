using FidenzCustomers.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;


namespace FidenzCustomers.Data
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
                    Id= customer.Index+1,
                    CustomerId = customer.Value.CustomerId,
                    Index = customer.Value.Index,
                    Age = customer.Value.Age,
                    EyeColor = customer.Value.EyeColor,
                    Name = customer.Value.Name,
                    Gender = customer.Value.Gender,
                    Company = customer.Value.Company,
                    Email = customer.Value.Email,
                    Phone = customer.Value.Phone,
                    About = customer.Value.About,
                    Registered = customer.Value.Registered,
                    Latitude = customer.Value.Latitude,
                    Longitude = customer.Value.Longitude,
                    Tag = customer.Value.Tag
                });

                modelBuilder.Entity<Address>().HasData(new
                {
                    AddressId = customer.Index+1,
                    Number = customer.Value.Address.Number,
                    Street = customer.Value.Address.Street,
                    City = customer.Value.Address.City,
                    State = customer.Value.Address.State,
                    Zip = customer.Value.Address.Zip
                });
            }
        }
    }
}

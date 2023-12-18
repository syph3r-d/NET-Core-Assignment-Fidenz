using FidenzCustomers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
            var jsonData = File.ReadAllText("Data/UserData.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(jsonData);
            foreach (var customer in customers)
            {
                modelBuilder.Entity<Customer>().HasData(new
                {
                    CustomerId = customer.CustomerId,
                    Index = customer.Index,
                    Age = customer.Age,
                    EyeColor = customer.EyeColor,
                    Name = customer.Name,
                    Gender = customer.Gender,
                    Company = customer.Company,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    About = customer.About,
                    Registered = customer.Registered,
                    Latitude = customer.Latitude,
                    Longitude = customer.Longitude,
                    Tag = customer.Tag
                });

                modelBuilder.Entity<Address>().HasData(new
                {
                    AddressId = customer.CustomerId,
                    Number = customer.Address.Number,
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    Zip = customer.Address.Zip
                });
            }
        }
    }
}

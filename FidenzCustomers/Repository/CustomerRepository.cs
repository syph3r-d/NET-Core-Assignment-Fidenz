using FidenzCustomers.Common.Interfaces;
using FidenzCustomers.Data;
using FidenzCustomers.Models;

namespace FidenzCustomers.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _dbContext.Customer.Update(customer);
        }
    }
}

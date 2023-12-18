using FidenzCustomers.Common.Interfaces;
using FidenzCustomers.Data;

namespace FidenzCustomers.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Customer = new CustomerRepository(_dbContext);
        }
        public ICustomerRepository Customer { get; private set; }

    }
}

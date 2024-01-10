using FidenzCustomers.Data.Models;

namespace FidenzCustomers.Data.Common.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void UpdateCustomer(Customer customer);
        void Save();
    }
}

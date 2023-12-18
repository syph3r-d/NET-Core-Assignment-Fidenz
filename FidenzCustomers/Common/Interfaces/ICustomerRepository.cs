using FidenzCustomers.Models;
using FidenzCustomers.Repository;

namespace FidenzCustomers.Common.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void UpdateCustomer(Customer customer);
        void Save();
    }
}

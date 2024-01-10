using FidenzCustomers.Application.DTOs;

namespace FidenzCustomers.Application.Common.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(string id);
        void UpdateCustomer(CustomerUpdateDto customer);
        IEnumerable<CustomerDto> SearchCustomers(string q);
    }
}

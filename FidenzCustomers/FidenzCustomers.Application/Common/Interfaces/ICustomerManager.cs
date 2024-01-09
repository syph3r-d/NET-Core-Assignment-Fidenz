using FidenzCustomers.Application.DTOs;
using FidenzCustomers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzCustomers.Application.Common.Interfaces
{
    public interface ICustomerManager
    {
        IEnumerable<CustomerDto> GetAllCustomers();
        CustomerDto GetCustomerById(string id);
        void UpdateCustomer(CustomerUpdateDto customer);
        IEnumerable<CustomerDto> SearchCustomers(string q);
    }
}

using FidenzCustomers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzCustomers.Data.Common.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void UpdateCustomer(Customer customer);
        void Save();
    }
}

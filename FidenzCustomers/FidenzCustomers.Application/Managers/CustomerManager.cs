using AutoMapper;
using FidenzCustomers.Application.Common.Interfaces;
using FidenzCustomers.Application.DTOs;
using FidenzCustomers.Data.Common.Interfaces;


namespace FidenzCustomers.Application.Managers
{
    

    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var res = _customerRepository.GetAll(null, "Address");
            return _mapper.Map<IEnumerable<CustomerDto>>(res);
        }

        public CustomerDto GetCustomerById(string id)
        {
           var res = _customerRepository.Get(c => c.CustomerId == id, "Address");
            return _mapper.Map<CustomerDto>(res);
        }

        public IEnumerable<CustomerDto> SearchCustomers(string q)
        {
            var res = _customerRepository.GetAll(c => c.Name.Contains(q) || c.EyeColor.Contains(q) || c.Company.Contains(q) || c.Email.Contains(q),"Address");
            return _mapper.Map<IEnumerable<CustomerDto>>(res);
        }

        public void UpdateCustomer(CustomerUpdateDto customer)
        {
           var oldCustomer = _customerRepository.Get(c => c.CustomerId == customer.CustomerId, "Address");
            var newCustomer = _mapper.Map(customer, oldCustomer);
            _customerRepository.UpdateCustomer(_mapper.Map(customer, oldCustomer));
        }


    }
}

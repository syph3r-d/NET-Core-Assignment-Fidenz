using FidenzCustomers.Application.DTOs;
using FidenzCustomers.Data.Models;
using AutoMapper;


namespace FidenzCustomers.Application.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerUpdateDto>();
            CreateMap<CustomerUpdateDto, Customer>()
                .ForAllMembers(o => o.Condition((src,dest,srcmember,destmember)=>srcmember !=null));
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }
    }
}

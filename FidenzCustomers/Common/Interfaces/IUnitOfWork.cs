namespace FidenzCustomers.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customer { get; }
    }
}

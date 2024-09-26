using FirstCompany.Data.Entities;

namespace FirstCompany.Data.Interface
{
    public interface ICustomerRepo
    {

        Task<List<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
        Task<Customer> GetCustomerByIdAsync(string customerId);
        Task<List<Customer>> SearchCustomersAsync(string? searchKeyword);
    }
}

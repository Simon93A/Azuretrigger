using FirstCompany.Data.Entities;
using FirstCompany.Data;
using FirstCompany.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCompany.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add customer
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteCustomerAsync(string id)
        {
            var customer = await GetCustomerByIdAsync(id);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        // Get all customers
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }

        // Get customer by ID
        public async Task<Customer> GetCustomerByIdAsync(string customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        // Search customer
        public async Task<List<Customer>> SearchCustomersAsync(string? searchKeyword)
        {
            //var customers = await _context.Customers
            //        .Where(c => c.Name.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase) || c.SalesPersonName.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase))
            //        .ToListAsync();

            searchKeyword = searchKeyword.ToLower();

            var customers = await _context.Customers
                    .Where(c => c.Name.Contains(searchKeyword) || c.SalesPersonName.Contains(searchKeyword))
                    .ToListAsync();

            return customers;
        }

        // update customer
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

    }
}

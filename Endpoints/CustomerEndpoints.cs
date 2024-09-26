using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using FirstCompany.Data.Entities;
using FirstCompany.Repos;
using FirstCompany.Data.Interface;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstCompany.Endpoints
{
    public static class CustomerEndpoints
    {
        // Ändrade till att inte ha dependency injection i klassens huvud, kan testa om det fungerar senare
        public static WebApplication MapCustomerEndpoints(this WebApplication app)
        {
            
            // Add a new Customer
            app.MapPost("api/customers", async (ICustomerRepo customerRepo, Customer customer) =>
            {
                await customerRepo.AddCustomerAsync(customer);
                return Results.Ok();

            });

           // Get all customers
            app.MapGet("api/customers", async (ICustomerRepo customerRepo) =>
            {
                var customers = await customerRepo.GetAllCustomersAsync();
                if (customers == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(customers);
            });

            // Update
            app.MapPut("/customers", async (ICustomerRepo customerRepo, Customer updatedCustomer) =>
            {
                await customerRepo.UpdateCustomerAsync(updatedCustomer);
            });

            // Delete Customer based on ID
            app.MapDelete("/customers/{id}", async (ICustomerRepo customerRepo, string id) =>
            {
                await customerRepo.DeleteCustomerAsync(id);
                return Results.Ok();
            });

            // Search by customer name or sales person name
            app.MapGet("api/customers/search", async (ICustomerRepo customerRepo, string searchKeyword) =>
            {
                var customers = await customerRepo.SearchCustomersAsync(searchKeyword);
                return Results.Ok(customers);
            });

            return app;
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using FirstCompany.Data.Entities;
using FirstCompany.Repos;
using FirstCompany.Data.Interface;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstCompany.Data;
using FirstCompany.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


RegisterServices(builder);

ConfigureSwagger(builder);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstCompany"));
app.UseHttpsRedirection();
app.MapCustomerEndpoints();

app.Run();

void RegisterServices(WebApplicationBuilder builder)
{
    AddDatabaseContext(builder);
    AddRepositories(builder);
}

void AddDatabaseContext(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseCosmos(
                    builder.Configuration["CosmosDb:AccountEndpoint"],
                    builder.Configuration["CosmosDb:AccountKey"],
                    builder.Configuration["CosmosDb:DatabaseName"]
                ));
}

void AddRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "FirstCompany", Version = "v1" });
    });
}


using HoneyRaesAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fix for cycle error
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Get servicetickets list contents
app.MapGet("/servicetickets", () =>
{
    return ServiceTicketObjects.serviceTickets;
});

// Get single service ticket by id
app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = ServiceTicketObjects.serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTicket.Employee = EmployeeObjects.employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeId);
    return Results.Ok(serviceTicket);
});

// Get employees list contents
app.MapGet("/employees", () =>
{
    return EmployeeObjects.employees;
});

// Get single employee by id
app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = EmployeeObjects.employees.FirstOrDefault(emp => emp.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    employee.ServiceTickets = ServiceTicketObjects.serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);

});

// Get customers list contents
app.MapGet("/customers", () =>
{
    return CustomersObjects.customers;
});

// Get single customer by id
app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = CustomersObjects.customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(customer);
});

app.Run();

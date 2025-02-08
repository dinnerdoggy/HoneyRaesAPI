using HoneyRaesAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// DRY variables
List<ServiceTicket> serviceTickets = ServiceTicketObjects.serviceTickets;
List<Customer> customers = CustomersObjects.customers;
List<Employee> employees = EmployeeObjects.employees;

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

// GET servicetickets list contents
app.MapGet("/servicetickets", () =>
{
    return serviceTickets;
});

// GET single service ticket by id
app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTicket.Employee = EmployeeObjects.employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeId);
    serviceTicket.Customer = CustomersObjects.customers.FirstOrDefault(c => c.Id == serviceTicket.CustomerId);
    return Results.Ok(serviceTicket);
});

// GET tickets filtered by Emergency = true, and by DateCompleted = null
app.MapGet("servicetickets/emergencies", () =>
{
    List<ServiceTicket> emergencyTickets = serviceTickets
    .Where(st => st.Emergency == true)
    .ToList();
    List<ServiceTicket> incompleteTickets = serviceTickets
    .Where(st => st.DateCompleted == null)
    .ToList();
    List<ServiceTicket> combinedList = emergencyTickets.Union(incompleteTickets).ToList();
    return combinedList;
});

// GET unassigned service tickets
app.MapGet("servicetickets/unassigned", () =>
{
    List<ServiceTicket> unassigned = serviceTickets
    .Where(st => st.EmployeeId == null)
    .ToList();
    return unassigned;
});

// GET completed tickets sorted by oldest first
app.MapGet("servicetickets/completed", () =>
{
    List<ServiceTicket> completed = serviceTickets
    .Where (st => st.DateCompleted != null)
    .OrderBy(st => st.DateCompleted)
    .ToList();
    return completed;
});

// GET incomplete tickets sorted by 1st: emergencies, 2nd: not assigned
app.MapGet("servicetickets/pending", () =>
{
    List<ServiceTicket> pendingST = serviceTickets
    .Where(st => st.DateCompleted == null) // null is considered the smallest value, so null sorts first
    .OrderByDescending(st => st.Emergency)
    .ThenBy(st => st.EmployeeId != null) // Even though null is considered the smallest value, this actually returns a bool of false, false = 0, true = 1
    .ToList();
    return pendingST;
});

// POST a service ticket
app.MapPost("/servicetickets", (ServiceTicket serviceTicket) =>
{
    // creates a new id (When we get to it later, our SQL database will do this for us like JSON Server did!)
    serviceTicket.Id = serviceTickets.Max(st => st.Id) + 1;
    serviceTickets.Add(serviceTicket);
    return serviceTicket;
});

// UPDATE a service ticket
app.MapPut("/servicetickets/{id}", (int id, ServiceTicket serviceTicket) =>
{
    ServiceTicket ticketToUpdate = serviceTickets.FirstOrDefault(st => st.Id == id);
    int ticketIndex = serviceTickets.IndexOf(ticketToUpdate);
    if (ticketToUpdate == null)
    {
        return Results.NotFound();
    }
    //the id in the request route doesn't match the id from the ticket in the request body. That's a bad request!
    if (id != serviceTicket.Id)
    {
        return Results.BadRequest();
    }
    serviceTickets[ticketIndex] = serviceTicket;
    return Results.Ok();
});

// UPDATE a ticket as completed
app.MapPut("/servicetickets/{id}/complete", (int id) =>
{
    ServiceTicket ticketToComplete = serviceTickets.FirstOrDefault(st => st.Id == id);
    ticketToComplete.DateCompleted = DateTime.Today;
});

// DELETE a service ticket
app.MapDelete("servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    serviceTickets.Remove(serviceTicket);
    return Results.NoContent();
});

// GET employees list contents
app.MapGet("/employees", () =>
{
    return EmployeeObjects.employees;
});

// GET single employee by id
app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = EmployeeObjects.employees.FirstOrDefault(emp => emp.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);

});

// GET Available employees (emp's that have no tickets assigned to them)
app.MapGet("/employees/available", () =>
{
    List<Employee> availableEmps = employees
    .Where(e => e.ServiceTickets != null && e.ServiceTickets.Count == 0)
    .ToList();
    return availableEmps;
});

// GET Employees customers
app.MapGet("employees/{id}/customers", (int id) =>
{
    Employee selectedEmployee = employees.FirstOrDefault(emp => emp.Id == id);
    List<Customer> empCust = selectedEmployee.ServiceTickets
    .Where(st => st.Customer != null)
    .Select(st => st.Customer)
    .Distinct()
    .ToList();
    return empCust;
});

// GET Employee of the month (most service tickets completed)
app.MapGet("employees/eotm", () =>
{
    DateTime pastMonth = DateTime.UtcNow.AddMonths(-1);

    Employee eotm = employees
        .Where(e => e.ServiceTickets != null)
        .OrderByDescending(e => e.ServiceTickets.Count(st => st.DateCompleted >= pastMonth))
        .FirstOrDefault();

    return eotm;
});



// GET customers list contents
app.MapGet("/customers", () =>
{
    return CustomersObjects.customers;
});

// GET single customer by id
app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = CustomersObjects.customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    customer.ServiceTickets = serviceTickets.Where(st => st.CustomerId == id).ToList();
    return Results.Ok(customer);
});

// GET Inactive Customers (No tickets completed in 1 year)
app.MapGet("/customers/inactive", () =>
{
    DateTime aYearOrMore = DateTime.Now.AddYears(-1);
    List<Customer> inactiveCustomers = customers
    .Where(c => c.ServiceTickets != null && c.ServiceTickets.All(dt => dt.DateCompleted <= aYearOrMore))
    .ToList();
    return inactiveCustomers;
});

app.Run();

using HoneyRaesAPI.Models;

// Customers
Customer customer1 = new Customer(01, "Samuel", "Hermitage TN");
Customer customer2 = new Customer(02, "Harry", "Mt Juliet TN");
Customer customer3 = new Customer(03, "Tanner", "Murfreesboro TN");

// Employees
Employee employee1 = new Employee(11, "Will", "Kitting Supervisor");
Employee employee2 = new Employee(12, "Casey", "Dock Lead");

// Service Tickets
ServiceTicket ticket1 = new ServiceTicket(21, 02, 12, "Was a great help and deserves a raise", false, DateTime.Now);
ServiceTicket ticket2 = new ServiceTicket(22, 03, 11, "Skippity bop-boo, ticket no. two", false, DateTime.Now);
ServiceTicket ticket3 = new ServiceTicket(23, 01, 12, "Hippity sip-tea, ticket no. three", true, DateTime.Now);
ServiceTicket ticket4 = new ServiceTicket(24, 02, 11, "Clorpity snorp-pour, ticket no. four", false, DateTime.Now);
ServiceTicket ticket5 = new ServiceTicket(25, 03, 12, "Skappity clap-jive, ticket no. five", true, DateTime.Now);

// List of Customers
List<Customer> customers = new List<Customer> { customer1, customer2, customer3 };

// List of Employees
List<Employee> employees = new List<Employee> { employee1, employee2 };

// List of Service Tickets
List<ServiceTicket> serviceTickets = new List<ServiceTicket> { ticket1, ticket2, ticket3, ticket4, ticket5 };

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/servicetickets", () =>
{
    return serviceTickets;
});

app.MapGet("/servicetickets/{id}", (int id) =>
{
    return serviceTickets.FirstOrDefault(st => st.Id == id);
});

app.MapGet("/employees", () =>
{
    return employees;
});

app.Run();

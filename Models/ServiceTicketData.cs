using System.Text.Json.Serialization;

namespace HoneyRaesAPI.Models;

public class ServiceTicketObjects
{
    // Service Tickets
    public static ServiceTicket ticket1 = new ServiceTicket { Id = 21, CustomerId = 02, /*EmployeeId = 12,*/ Description = "Was a great help and deserves a raise", Emergency = false, /*DateCompleted = DateTime.Now, Employee = EmployeeObjects.employee2,*/ Customer = CustomersObjects.customer2 };
    public static ServiceTicket ticket2 = new ServiceTicket { Id = 22, CustomerId = 03, EmployeeId = 11, Description = "Skippity bop-boo, ticket no. two", Emergency = false, DateCompleted = DateTime.MinValue, Employee = EmployeeObjects.employee1, Customer = CustomersObjects.customer3 };
    public static ServiceTicket ticket3 = new ServiceTicket{ Id = 23, CustomerId = 01, EmployeeId = 12, Description = "Hippity sip-tea, ticket no. three", Emergency = true, DateCompleted = DateTime.Now, Employee = EmployeeObjects.employee2, Customer = CustomersObjects.customer1 };
    public static ServiceTicket ticket4 = new ServiceTicket{ Id = 24, CustomerId = 02, EmployeeId = 11, Description = "Clorpity snorp-pour, ticket no. four", Emergency = false, DateCompleted = DateTime.Now, Employee = EmployeeObjects.employee1, Customer = CustomersObjects.customer2 };
    public static ServiceTicket ticket5 = new ServiceTicket{ Id = 25, CustomerId = 03, EmployeeId = 12, Description = "Skappity clap-jive, ticket no. five", Emergency = true, DateCompleted = DateTime.MinValue, Employee = EmployeeObjects.employee2, Customer = CustomersObjects.customer3 };

    // List of Service Tickets
    public static List<ServiceTicket> serviceTickets = new List<ServiceTicket> { ticket1, ticket2, ticket3, ticket4, ticket5 };
}
using System.Text.Json.Serialization;

namespace HoneyRaesAPI.Models;

public class ServiceTicketObjects
{
    // Service Tickets
    public static ServiceTicket ticket1 = new ServiceTicket(21, 02, 12, "Was a great help and deserves a raise", false, DateTime.Now, EmployeeObjects.employee2, CustomersObjects.customer2);
    public static ServiceTicket ticket2 = new ServiceTicket(22, 03, 11, "Skippity bop-boo, ticket no. two", false, DateTime.Now, EmployeeObjects.employee1, CustomersObjects.customer3);
    public static ServiceTicket ticket3 = new ServiceTicket(23, 01, 12, "Hippity sip-tea, ticket no. three", true, DateTime.Now, EmployeeObjects.employee2, CustomersObjects.customer1);
    public static ServiceTicket ticket4 = new ServiceTicket(24, 02, 11, "Clorpity snorp-pour, ticket no. four", false, DateTime.Now, EmployeeObjects.employee1, CustomersObjects.customer2);
    public static ServiceTicket ticket5 = new ServiceTicket(25, 03, 12, "Skappity clap-jive, ticket no. five", true, DateTime.Now, EmployeeObjects.employee2, CustomersObjects.customer3);

    // List of Service Tickets
    public static List<ServiceTicket> serviceTickets = new List<ServiceTicket> { ticket1, ticket2, ticket3, ticket4, ticket5 };
}
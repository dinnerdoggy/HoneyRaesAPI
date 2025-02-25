namespace HoneyRaesAPI.Models;

// Customers
public class CustomersObjects
{
    public static Customer customer1 = new Customer(01,"Samuel", "Hermitage TN", ServiceTicketObjects.serviceTickets);
    public static Customer customer2 = new Customer(02, "Harry", "Mt Juliet TN", ServiceTicketObjects.serviceTickets);
    public static Customer customer3 = new Customer(03, "Tanner", "Murfreesboro TN", ServiceTicketObjects.serviceTickets);

    // List of Customers
    public static List<Customer> customers = new List<Customer> { CustomersObjects.customer1, CustomersObjects.customer2, CustomersObjects.customer3 };
}
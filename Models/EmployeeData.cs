namespace HoneyRaesAPI.Models;

public class EmployeeObjects
{
    // Employees
    public static Employee employee1 = new Employee(11, "Will", "Kitting Supervisor", ServiceTicketObjects.serviceTickets);
    public static Employee employee2 = new Employee(12, "Casey", "Dock Lead", ServiceTicketObjects.serviceTickets);
    public static Employee employee3 = new Employee(13, "Evil Casey", "Dock Villain", ServiceTicketObjects.serviceTickets);

    // List of Employees
    public static List<Employee> employees = new List<Employee> { employee1, employee2, employee3 };
}
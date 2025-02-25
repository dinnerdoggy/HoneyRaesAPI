namespace HoneyRaesAPI.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public List<ServiceTicket> ServiceTickets { get; set; }

    // Constructor
    public Employee() { }
    public Employee(int id, string name, string specialty, List<ServiceTicket> serviceTickets )
    {
        Id = id;
        Name = name;
        Specialty = specialty;
        ServiceTickets = serviceTickets;
    }
}
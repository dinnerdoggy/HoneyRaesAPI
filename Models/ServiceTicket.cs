namespace HoneyRaesAPI.Models;

public class ServiceTicket
{
    public int? Id { get; set; }
    public int? CustomerId { get; set; }
    public int? EmployeeId { get; set; }
    public string? Description { get; set; }
    public bool? Emergency { get; set; }
    public DateTime? DateCompleted { get; set; }
    public Employee? Employee { get; set; }
    public Customer? Customer { get; set; }

    //// Constructor - Commented out for the purpose of creating incomplete service tickets
    //public ServiceTicket() { }
    //public ServiceTicket(int id, int customerId, int employeeId, string description, bool emergency, DateTime dateCompleted, Employee employee, Customer customer)
    //{
    //    Id = id;
    //    CustomerId = customerId;
    //    EmployeeId = employeeId;
    //    Description = description;
    //    Emergency = emergency;
    //    DateCompleted = dateCompleted;
    //    Employee = employee;
    //    Customer = customer;
    //}
}

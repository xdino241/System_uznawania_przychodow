namespace System_uznawania_przychodow.Entities;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Login { get; set; } =  string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
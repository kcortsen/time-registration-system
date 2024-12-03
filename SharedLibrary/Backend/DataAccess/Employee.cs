using System.Text.Json.Serialization;

namespace SharedLibrary.Backend.DataAccess;

public class Employee
{
    public int EmployeeID { get; set; }  
    public string Initials { get; set; } = string.Empty;  
    public string CPRNumber { get; set; } = string.Empty;  
    public string Name { get; set; } = string.Empty;  

    
    public int DepartmentID { get; set; }  
    
    [JsonIgnore]
    public Department? Department { get; set; }  
    public ICollection<TimeRegistration> TimeRegistrations { get; set; } = new List<TimeRegistration>();  
}
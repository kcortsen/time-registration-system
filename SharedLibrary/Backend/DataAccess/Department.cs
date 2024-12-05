namespace SharedLibrary.Backend.DataAccess;

public class Department
{
    public int DepartmentID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; }


    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Case> Cases { get; set; } = new List<Case>();
}
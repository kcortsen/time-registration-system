using SharedLibrary.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace SharedLibrary.Backend.BusinessLogic;

public class EmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetAllEmployees()
    {
        return _context.Employees.Include(e => e.Department).ToList();
    }

    public Employee? GetEmployeeById(int id)
    {
        return _context.Employees.Include(e => e.Department).FirstOrDefault(e => e.EmployeeID == id);
    }

    public void AddEmployee(Employee employee)
    {
        if (_context.Employees.Any(e => e.Initials == employee.Initials))
            throw new Exception("Employee already exists");

        if (!_context.Departments.Any(d => d.DepartmentID == employee.DepartmentID))
            throw new Exception("Invalid department");

        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployee(Employee employee)
    {
        var existingEmployee = _context.Employees.Find(employee.EmployeeID);
        if (existingEmployee == null)
            throw new Exception("Employee not found");

        existingEmployee.Name = employee.Name;
        existingEmployee.Initials = employee.Initials;
        existingEmployee.CPRNumber = employee.CPRNumber;
        existingEmployee.DepartmentID = employee.DepartmentID;

        _context.SaveChanges();
    }

    public void DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
            throw new Exception("Employee not found");

        _context.Employees.Remove(employee);
        _context.SaveChanges();
    }

    public List<Department> GetAllDepartments()
    {
        return _context.Departments.ToList();
    }
}
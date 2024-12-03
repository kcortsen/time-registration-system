using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Backend.BusinessLogic;
using SharedLibrary.Backend.DataAccess;

namespace MyMauiApp.ViewModels;

public class EmployeeViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;

    public ObservableCollection<Employee> EmployeeList { get; set; }
    public ObservableCollection<Department> DepartmentList { get; set; }
    public Department SelectedDepartment { get; set; }

    public Employee NewEmployee { get; set; } = new Employee();

    public ICommand SaveEmployeeCommand { get; }

    public EmployeeViewModel(AppDbContext context)
    {
        _context = context;

        EmployeeList = new ObservableCollection<Employee>(_context.Employees.Include(e => e.Department).ToList());
        DepartmentList = new ObservableCollection<Department>(_context.Departments.ToList());

        SaveEmployeeCommand = new Command(SaveEmployee);
    }

    private void SaveEmployee()
    {
        try
        {
            if (SelectedDepartment == null)
                throw new Exception("You need to select a department");

            NewEmployee.DepartmentID = SelectedDepartment.DepartmentID;
            _context.Employees.Add(NewEmployee);
            _context.SaveChanges();

            EmployeeList.Add(NewEmployee);

            NewEmployee = new Employee();
            OnPropertyChanged(nameof(NewEmployee));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SharedLibrary.Backend.BusinessLogic;
using SharedLibrary.Backend.DataAccess;

public class EmployeeViewModel : INotifyPropertyChanged
{
    private readonly EmployeeService _employeeService;

    public ObservableCollection<Employee> EmployeeList { get; set; }
    public ObservableCollection<Department> DepartmentList { get; set; }
    public Department SelectedDepartment { get; set; }

    public Employee NewEmployee { get; set; } = new Employee();

    public ICommand SaveEmployeeCommand { get; }

    public EmployeeViewModel(EmployeeService employeeService)
    {
        _employeeService = employeeService;

        EmployeeList = new ObservableCollection<Employee>(_employeeService.GetAllEmployees());
        DepartmentList = new ObservableCollection<Department>(_employeeService.GetAllDepartments());

        SaveEmployeeCommand = new Command(SaveEmployee);
    }

    private void SaveEmployee()
    {
        try
        {
            if (SelectedDepartment == null)
                throw new Exception("You must select a department");

            NewEmployee.DepartmentID = SelectedDepartment.DepartmentID;
            _employeeService.AddEmployee(NewEmployee);

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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Backend.BusinessLogic;
using SharedLibrary.Backend.DataAccess;

namespace MyMauiApp.ViewModels;

public class OverviewViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;

    public ObservableCollection<Employee> EmployeeOverviewList { get; set; }

    public OverviewViewModel(AppDbContext context)
    {
        _context = context;

        EmployeeOverviewList = new ObservableCollection<Employee>();
        LoadOverview();
    }

    private void LoadOverview()
    {
        var employees = _context.Employees
            .Include(e => e.TimeRegistrations)
            .ThenInclude(tr => tr.Case)
            .ToList();

        EmployeeOverviewList.Clear();
        foreach (var employee in employees)
        {
            EmployeeOverviewList.Add(employee);
        }

        OnPropertyChanged(nameof(EmployeeOverviewList));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
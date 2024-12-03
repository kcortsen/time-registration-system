using SharedLibrary.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SharedLibrary.Backend.BusinessLogic;

public class TimeRegistrationService
{
    private readonly AppDbContext _context;

    public TimeRegistrationService(AppDbContext context)
    {
        _context = context;
    }

    public List<TimeRegistration> GetAllTimeRegistrations()
    {
        return _context.TimeRegistrations.Include(tr => tr.Employee).Include(tr => tr.Case).ToList();
    }

    public double GetTotalHoursForWeek(int employeeId, DateTime weekBegin, DateTime weekEnd)
    {
        var registrations = _context.TimeRegistrations
            .Where(tr => tr.EmployeeID == employeeId && tr.StartTime >= weekBegin && tr.EndTime <= weekEnd)
            .ToList();

        return registrations.Sum(tr => (tr.EndTime - tr.StartTime).TotalHours);
    }

    public void AddTimeRegistration(TimeRegistration registration)
    {
        if (!_context.Employees.Any(e => e.EmployeeID == registration.EmployeeID))
            throw new Exception("Invalid employee");

        if (registration.StartTime >= registration.EndTime)
            throw new Exception("Start time must be before the end time");

        var weekStart = registration.StartTime.Date.AddDays(-(int)registration.StartTime.DayOfWeek);
        var weekEnd = weekStart.AddDays(7);

        var existingHours = GetTotalHoursForWeek(registration.EmployeeID, weekStart, weekEnd);
        var newHours = (registration.EndTime - registration.StartTime).TotalHours;

        if (existingHours + newHours > 37)
        {
            throw new Exception(
                $"The total registered hours for this week ({existingHours + newHours} hours) is more than the 37 hour limit");
        }

        _context.TimeRegistrations.Add(registration);
        _context.SaveChanges();
    }

    public void DeleteTimeRegistration(int id)
    {
        var registration = _context.TimeRegistrations.Find(id);
        if (registration == null)
            throw new Exception("Time registration not found");

        _context.TimeRegistrations.Remove(registration);
        _context.SaveChanges();
    }
}
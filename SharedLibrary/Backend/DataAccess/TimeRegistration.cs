using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Backend.DataAccess;

public class TimeRegistration
{
    public int TimeRegistrationID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int EmployeeID { get; set; }
    public Employee? Employee { get; set; }

    public int? CaseID { get; set; }
    public Case? Case { get; set; }
}
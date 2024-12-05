using System.Text.Json.Serialization;

namespace SharedLibrary.Backend.DataAccess;

public class Case
{
    public int CaseID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    public int DepartmentID { get; set; }

    [JsonIgnore] public Department? Department { get; set; }
    public ICollection<TimeRegistration> TimeRegistrations { get; set; } = new List<TimeRegistration>();
}
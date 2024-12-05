using System.Collections.ObjectModel;
using System.ComponentModel;
using SharedLibrary.Backend.BusinessLogic;
using SharedLibrary.Backend.DataAccess;
using System.Globalization;

namespace MyMauiApp.ViewModels;

public class SummaryViewModel : INotifyPropertyChanged
{
    private readonly TimeRegistrationService _timeRegistrationService;

    public ObservableCollection<WeeklySummary> WeeklySummaries { get; set; }
    public ObservableCollection<MonthlySummary> MonthlySummaries { get; set; }
    public TimeSpan TotalTime { get; set; }

    public SummaryViewModel(TimeRegistrationService timeRegistrationService)
    {
        _timeRegistrationService = timeRegistrationService;

        WeeklySummaries = new ObservableCollection<WeeklySummary>();
        MonthlySummaries = new ObservableCollection<MonthlySummary>();

        LoadSummaries();
    }

    private void LoadSummaries()
    {
        var registrations = _timeRegistrationService.GetAllTimeRegistrations();

        var weeklyGroups = registrations
            .GroupBy(tr =>
                CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(tr.StartTime, CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday))
            .Select(g => new WeeklySummary
            {
                WeekNumber = g.Key,
                TotalTime = g.Aggregate(TimeSpan.Zero, (sum, tr) => sum + (tr.EndTime - tr.StartTime))
            });

        var monthlyGroups = registrations
            .GroupBy(tr => new { tr.StartTime.Year, tr.StartTime.Month })
            .Select(g => new MonthlySummary
            {
                Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                TotalTime = g.Aggregate(TimeSpan.Zero, (sum, tr) => sum + (tr.EndTime - tr.StartTime))
            });

        TotalTime = registrations.Aggregate(TimeSpan.Zero, (sum, tr) => sum + (tr.EndTime - tr.StartTime));

        WeeklySummaries.Clear();
        foreach (var summary in weeklyGroups)
        {
            WeeklySummaries.Add(summary);
        }

        MonthlySummaries.Clear();
        foreach (var summary in monthlyGroups)
        {
            MonthlySummaries.Add(summary);
        }

        OnPropertyChanged(nameof(WeeklySummaries));
        OnPropertyChanged(nameof(MonthlySummaries));
        OnPropertyChanged(nameof(TotalTime));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class WeeklySummary
{
    public int WeekNumber { get; set; }
    public TimeSpan TotalTime { get; set; }
}

public class MonthlySummary
{
    public string Month { get; set; }
    public TimeSpan TotalTime { get; set; }
}
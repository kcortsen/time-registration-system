using MyMauiApp.Views;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnNavigateToEmployees(object sender, EventArgs e)
    {
        var employeeView = ActivatorUtilities.CreateInstance<EmployeeView>(ServiceProviderHelper.Current);
        await Navigation.PushAsync(employeeView);
    }

    private async void OnNavigateToOverview(object sender, EventArgs e)
    {
        var overviewView = ActivatorUtilities.CreateInstance<OverviewView>(ServiceProviderHelper.Current);
        await Navigation.PushAsync(overviewView);
    }

    private async void OnNavigateToSummary(object sender, EventArgs e)
    {
        var summaryView = ActivatorUtilities.CreateInstance<SummaryView>(ServiceProviderHelper.Current);
        await Navigation.PushAsync(summaryView);
    }

    private async void OnNavigateToCases(object sender, EventArgs e)
    {
        var caseManagementView = ActivatorUtilities.CreateInstance<CaseView>(ServiceProviderHelper.Current);
        await Navigation.PushAsync(caseManagementView);
    }
}
using MyMauiApp.Views;
using SharedLibrary.Backend.BusinessLogic;

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
}
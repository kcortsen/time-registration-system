using MyMauiApp.ViewModels;

namespace MyMauiApp.Views;

public partial class EmployeeView : ContentPage
{
    public EmployeeView(EmployeeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
using MyMauiApp.ViewModels;

namespace MyMauiApp.Views;

public partial class OverviewView : ContentPage
{
    public OverviewView(OverviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; 
    }
}
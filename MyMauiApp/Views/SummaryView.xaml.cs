using MyMauiApp.ViewModels;

namespace MyMauiApp.Views;

public partial class SummaryView : ContentPage
{
    public SummaryView(SummaryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
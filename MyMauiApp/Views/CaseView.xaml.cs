using MyMauiApp.ViewModels;

namespace MyMauiApp.Views;

public partial class CaseView : ContentPage
{
    public CaseView(CaseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SharedLibrary.Backend.BusinessLogic;
using SharedLibrary.Backend.DataAccess;

namespace MyMauiApp.ViewModels;

public class CaseViewModel : INotifyPropertyChanged
{
    private readonly CaseService _caseService;
    private bool _isEditPopupVisible;

    public ObservableCollection<Case> CaseList { get; set; }
    public Case SelectedCase { get; set; }
    public Case NewCase { get; set; }

    public ICommand AddCaseCommand { get; }
    public ICommand UpdateCaseCommand { get; }
    public ICommand DeleteCaseCommand { get; }
    public ICommand OpenEditCaseCommand { get; }
    public ICommand CloseEditPopupCommand { get; }

    public bool IsEditPopupVisible
    {
        get => _isEditPopupVisible;
        set
        {
            if (_isEditPopupVisible != value)
            {
                _isEditPopupVisible = value;
                OnPropertyChanged(nameof(IsEditPopupVisible));
            }
        }
    }

    public CaseViewModel(CaseService caseService)
    {
        _caseService = caseService;
        CaseList = new ObservableCollection<Case>(_caseService.GetAllCases());
        SelectedCase = new Case();
        NewCase = new Case();

        AddCaseCommand = new Command(AddCase);
        UpdateCaseCommand = new Command(UpdateCase);
        DeleteCaseCommand = new Command<Case>(DeleteCase);
        OpenEditCaseCommand = new Command<Case>(OpenEditCase);
        CloseEditPopupCommand = new Command(CloseEditPopup);
    }

    private void OpenEditCase(Case caseToEdit)
    {
        SelectedCase = caseToEdit;
        IsEditPopupVisible = true;
    }

    private void CloseEditPopup()
    {
        IsEditPopupVisible = false;
    }

    private void AddCase()
    {
        try
        {
            _caseService.AddCase(NewCase);
            CaseList.Add(NewCase);
            NewCase = new Case();
            OnPropertyChanged(nameof(NewCase));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding case: {ex.Message}");
        }
    }

    private void UpdateCase()
    {
        try
        {
            _caseService.UpdateCase(SelectedCase);
            var index = CaseList.IndexOf(CaseList.First(c => c.CaseID == SelectedCase.CaseID));
            if (index >= 0)
            {
                CaseList[index] = SelectedCase;
                OnPropertyChanged(nameof(CaseList));
            }

            IsEditPopupVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating case: {ex.Message}");
        }
    }

    private void DeleteCase(Case caseToDelete)
    {
        try
        {
            _caseService.DeleteCase(caseToDelete.CaseID);
            CaseList.Remove(caseToDelete);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting case: {ex.Message}");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
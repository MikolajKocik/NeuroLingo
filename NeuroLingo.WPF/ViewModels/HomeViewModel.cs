namespace NeuroLingo.WPF.ViewModels;

/// <summary>
/// ViewModel for the Home view.
/// </summary>
public class HomeViewModel : ViewModelBase
{
    private string _title = "Ucz się szybciej. Zapamiętuj więcej";
    private string _description = "NeuroLingo to platforma oparta o badania naukowe, która dostosowuje tempo nauki do Ciebie, aby maksymalizować retencję informacji.";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
}

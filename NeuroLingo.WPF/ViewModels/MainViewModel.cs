using System.Windows.Input;
using NeuroLingo.WPF.Commands;

namespace NeuroLingo.WPF.ViewModels;

/// <summary>
/// Main ViewModel for application navigation and state management.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;
    private bool _isAuthenticated;
    private string _welcomeMessage = string.Empty;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public bool IsAuthenticated
    {
        get => _isAuthenticated;
        set => SetProperty(ref _isAuthenticated, value);
    }

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set => SetProperty(ref _welcomeMessage, value);
    }

    public ICommand ShowLoginCommand { get; }
    public ICommand ShowRegisterCommand { get; }
    public ICommand ShowHomeCommand { get; }
    public ICommand LogoutCommand { get; }

    public MainViewModel()
    {
        _currentViewModel = new HomeViewModel();

        ShowLoginCommand = new RelayCommand(_ => ShowLogin());
        ShowRegisterCommand = new RelayCommand(_ => ShowRegister());
        ShowHomeCommand = new RelayCommand(_ => ShowHome());
        LogoutCommand = new RelayCommand(_ => Logout());
    }

    private void ShowLogin()
    {
        var loginViewModel = new LoginViewModel();
        loginViewModel.LoginSuccessful += OnLoginSuccessful;
        loginViewModel.NavigateToRegister += OnNavigateToRegister;
        CurrentViewModel = loginViewModel;
    }

    private void ShowRegister()
    {
        var registerViewModel = new RegisterViewModel();
        registerViewModel.RegisterSuccessful += OnRegisterSuccessful;
        registerViewModel.NavigateToLogin += OnNavigateToLogin;
        CurrentViewModel = registerViewModel;
    }

    private void ShowHome()
    {
        CurrentViewModel = new HomeViewModel();
    }

    private void Logout()
    {
        IsAuthenticated = false;
        WelcomeMessage = string.Empty;
        ShowHome();
    }

    private void OnLoginSuccessful(object? sender, EventArgs e)
    {
        if (sender is LoginViewModel vm)
        {
            IsAuthenticated = true;
            WelcomeMessage = $"Welcome, {vm.Email}!";
            ShowHome();
        }
    }

    private void OnRegisterSuccessful(object? sender, EventArgs e)
    {
        // After successful registration, show login
        ShowLogin();
    }

    private void OnNavigateToRegister(object? sender, EventArgs e)
    {
        ShowRegister();
    }

    private void OnNavigateToLogin(object? sender, EventArgs e)
    {
        ShowLogin();
    }
}

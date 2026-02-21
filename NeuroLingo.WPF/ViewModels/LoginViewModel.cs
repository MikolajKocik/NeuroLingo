using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using NeuroLingo.WPF.Commands;

namespace NeuroLingo.WPF.ViewModels;

/// <summary>
/// ViewModel for user login functionality.
/// </summary>
public class LoginViewModel : ViewModelBase
{
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isLoading;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    [Required(ErrorMessage = "Password is required")]
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public ICommand LoginCommand { get; }
    public ICommand NavigateToRegisterCommand { get; }

    public event EventHandler? LoginSuccessful;
    public event EventHandler? NavigateToRegister;

    public LoginViewModel()
    {
        LoginCommand = new AsyncRelayCommand(ExecuteLogin, CanExecuteLogin);
        NavigateToRegisterCommand = new RelayCommand(_ => NavigateToRegister?.Invoke(this, EventArgs.Empty));
    }

    private bool CanExecuteLogin(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !IsLoading;
    }

    private async Task ExecuteLogin(object? parameter)
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            // Simulate authentication - in real app, call auth service
            await Task.Delay(1000);

            // Simple validation for demo
            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Email is not valid";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password is required";
                return;
            }

            // Raise success event
            LoginSuccessful?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Login failed: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

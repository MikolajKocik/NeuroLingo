using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows.Input;
using NeuroLingo.WPF.Commands;

namespace NeuroLingo.WPF.ViewModels;

/// <summary>
/// ViewModel for user registration functionality.
/// </summary>
public class RegisterViewModel : ViewModelBase
{
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isLoading;
    private bool _hasMinLength;
    private bool _hasUppercase;
    private bool _hasDigit;

    private static readonly Regex PasswordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", RegexOptions.Compiled);

    [Required]
    [EmailAddress]
    [Display(Name = "Email address")]
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    [Required]
    [DataType(DataType.Password)]
    public string Password
    {
        get => _password;
        set
        {
            if (SetProperty(ref _password, value))
            {
                ValidatePasswordRules();
            }
        }
    }

    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
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

    public bool HasMinLength
    {
        get => _hasMinLength;
        set => SetProperty(ref _hasMinLength, value);
    }

    public bool HasUppercase
    {
        get => _hasUppercase;
        set => SetProperty(ref _hasUppercase, value);
    }

    public bool HasDigit
    {
        get => _hasDigit;
        set => SetProperty(ref _hasDigit, value);
    }

    public ICommand RegisterCommand { get; }
    public ICommand NavigateToLoginCommand { get; }

    public event EventHandler? RegisterSuccessful;
    public event EventHandler? NavigateToLogin;

    public RegisterViewModel()
    {
        RegisterCommand = new AsyncRelayCommand(ExecuteRegister, CanExecuteRegister);
        NavigateToLoginCommand = new RelayCommand(_ => NavigateToLogin?.Invoke(this, EventArgs.Empty));
    }

    private void ValidatePasswordRules()
    {
        HasMinLength = Password.Length >= 8;
        HasUppercase = Password.Any(char.IsUpper);
        HasDigit = Password.Any(char.IsDigit);
    }

    private bool CanExecuteRegister(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Email) &&
               !string.IsNullOrWhiteSpace(Password) &&
               !string.IsNullOrWhiteSpace(ConfirmPassword) &&
               !IsLoading;
    }

    private async Task ExecuteRegister(object? parameter)
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            // Simulate registration - in real app, call auth service
            await Task.Delay(1000);

            // Validation
            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Email is not valid";
                return;
            }

            if (!PasswordRegex.IsMatch(Password))
            {
                ErrorMessage = "Password must be at least 8 characters long, contain uppercase, lowercase and a digit.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return;
            }

            // Raise success event
            RegisterSuccessful?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Registration failed: {ex.Message}";
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

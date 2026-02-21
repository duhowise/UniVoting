using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.ViewModels;

public partial class AdminLoginViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;
    private readonly IAdminSessionService _session;

    public event Action? LoginSucceeded;
    public event Action<string>? ErrorOccurred;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private bool _isLoginEnabled = true;

    public AdminLoginViewModel(IElectionConfigurationService electionService, IAdminSessionService session)
    {
        _electionService = electionService;
        _session = session;
    }

    [RelayCommand(CanExecute = nameof(IsLoginEnabled))]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorOccurred?.Invoke("Please Enter Username or password to Login.");
            ClearCredentials();
            return;
        }
        try
        {
            IsLoginEnabled = false;
            var admin = await _electionService.Login(new Comissioner { UserName = Username, Password = Password });
            if (admin != null)
            {
                _session.CurrentAdmin = admin;
                LoginSucceeded?.Invoke();
            }
            else
            {
                ErrorOccurred?.Invoke("Wrong username or password.");
                ClearCredentials();
            }
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke($"Could not connect to the database.\n\n{ex.Message}");
        }
        finally
        {
            IsLoginEnabled = true;
        }
    }

    public void ClearCredentials()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
}

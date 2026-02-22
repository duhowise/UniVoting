using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class PresidentLoginViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;

    public event Action? LoginSucceeded;
    public event Action<string>? ErrorOccurred;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private bool _isLoginEnabled = true;

    public PresidentLoginViewModel(IElectionConfigurationService electionService)
    {
        _electionService = electionService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorOccurred?.Invoke("Wrong username or password.");
            ClearCredentials();
            return;
        }
        IsLoginEnabled = false;
        try
        {
            var president = await _electionService.Login(new Comissioner { UserName = Username, Password = Password, IsPresident = true });
            if (president != null)
            {
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
            ClearCredentials();
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

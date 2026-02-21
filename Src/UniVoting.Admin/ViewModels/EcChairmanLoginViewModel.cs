using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.ViewModels;

public partial class EcChairmanLoginViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;

    public event Action? LoginSucceeded;
    public event Action<string>? ErrorOccurred;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    public EcChairmanLoginViewModel(IElectionConfigurationService electionService)
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
        var chairman = await _electionService.Login(new Comissioner { UserName = Username, Password = Password, IsChairman = true });
        if (chairman != null)
        {
            LoginSucceeded?.Invoke();
        }
        else
        {
            ErrorOccurred?.Invoke("Wrong username or password.");
            ClearCredentials();
        }
    }

    public void ClearCredentials()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
}

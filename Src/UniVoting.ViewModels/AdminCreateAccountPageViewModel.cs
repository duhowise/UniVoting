using System;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class AdminCreateAccountPageViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;

    public event Action<string>? SuccessOccurred;
    public event Action<string>? ErrorOccurred;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _repeatPassword;

    [ObservableProperty]
    private bool _isChairman;

    [ObservableProperty]
    private bool _isPresident;

    [ObservableProperty]
    private bool _passwordsMatch = true;

    public AdminCreateAccountPageViewModel(IElectionConfigurationService electionService)
    {
        _electionService = electionService;
    }

    partial void OnPasswordChanged(string? value) => CheckPasswordMatch();
    partial void OnRepeatPasswordChanged(string? value) => CheckPasswordMatch();

    partial void OnIsChairmanChanged(bool value)
    {
        if (value) IsPresident = false;
    }

    partial void OnIsPresidentChanged(bool value)
    {
        if (value) IsChairman = false;
    }

    private void CheckPasswordMatch()
    {
        PasswordsMatch = string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RepeatPassword) ||
                         Password == RepeatPassword;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password))
        {
            try
            {
                await _electionService.SaveComissioner(new Comissioner
                {
                    FullName = Name ?? string.Empty,
                    UserName = Username ?? string.Empty,
                    Password = Password ?? string.Empty,
                    IsChairman = IsChairman,
                    IsPresident = IsPresident
                });
                SuccessOccurred?.Invoke($"{Username} Successfully created");
                ClearForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ErrorOccurred?.Invoke("Something Went Wrong");
            }
        }
    }

    public void ClearForm()
    {
        Name = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
        RepeatPassword = string.Empty;
        IsChairman = false;
        IsPresident = false;
    }
}

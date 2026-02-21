using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Model;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminMenuPage : UserControl
    {
        public Comissioner Comissioner { get; }

        public AdminMenuPage() { InitializeComponent(); Comissioner = new Comissioner(); }

        public AdminMenuPage(Comissioner comissioner)
        {
            Comissioner = comissioner;
            InitializeComponent();
            BtnDeclareVotes.Click += BtnDeclareVotes_Click;
            BtnDispensePassword.Click += BtnDispensePassword_Click;
            Loaded += AdminMenuPage_Loaded;
        }

        private void BtnDispensePassword_Click(object? sender, RoutedEventArgs e)
        {
            new AdminDispensePasswordWindow().Show();
        }

        private void AdminMenuPage_Loaded(object? sender, RoutedEventArgs e)
        {
            if (Comissioner.IsChairman)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
            }
            else if (Comissioner.IsPresident)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
                BtnSetUpVoters.IsEnabled = false;
            }
            else if (!Comissioner.IsChairman && !Comissioner.IsAdmin && !Comissioner.IsPresident)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
                BtnDeclareVotes.IsEnabled = false;
            }
        }

        private void BtnDeclareVotes_Click(object? sender, RoutedEventArgs e)
        {
            new PresidentLoginWindow().Show();
        }

        private void BtnCreateAccount_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(new AdminCreateAccountPage());
        }

        private void BtnSetUpElection_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(new AdminSetUpElectionPage());
        }

        private void BtnSetUpPostions_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(new AdminSetUpPositionPage());
        }

        private void BtnSetUpCandidates_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(new AdminSetUpCandidatesPage());
        }

        private void BtnSetUpVoters_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(new AdminAddVotersPage());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class ClientsLoginWindow : Window
    {
        private IEnumerable<Model.Position> _positions;
        private Stack<Model.Position> _positionsStack;
        private Voter _voter;

        public ClientsLoginWindow()
        {
            InitializeComponent();
            _positionsStack = new Stack<Model.Position>();
            Loaded += ClientsLoginWindow_Loaded;
            _voter = new Voter();
            BtnGo.IsDefault = true;
            BtnGo.Click += BtnGo_Click;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void ClientsLoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var election = ElectionConfigurationService.ConfigureElection();
                if (election?.Logo != null)
                    MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) { Opacity = 0.2 };
                if (election != null)
                {
                    VotingName.Text = election.ElectionName?.ToUpper();
                    VotingSubtitle.Content = election.EletionSubTitle?.ToUpper();
                }
                _positions = ElectionConfigurationService.GetAllPositions();
                foreach (var position in _positions)
                {
                    _positionsStack.Push(position);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Election Positions Error");
            }
        }

        private async void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Pin.Text))
            {
                try
                {
                    _voter = await ElectionConfigurationService.LoginVoter(new Voter { VoterCode = Pin.Text });
                    ConfirmVoterAsync();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Election Login Error");
                    throw;
                }
            }
        }

        public void ConfirmVoterAsync()
        {
            if (_voter != null)
            {
                if (!_voter.VoteInProgress && !_voter.Voted)
                {
                    new MainWindow(_positionsStack, _voter).Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Please Contact Admin Your Details May Not Be Available\n Possible Cause: You May Have Already Voted",
                        "Error Confirming Voter");
                    Pin.Text = String.Empty;
                }
            }
            else
            {
                MessageBox.Show("Wrong Code!", "Error Confirming Voter");
                Pin.Text = String.Empty;
            }
        }
    }
}

﻿using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientsLoginWindow.xaml
    /// </summary>
    public partial class ClientsLoginWindow : MetroWindow
    {
        private IEnumerable<Model.Position> _positions;
         private Stack<Model.Position> _positionsStack;
        private Voter _voter;
        public ClientsLoginWindow()
        {
            InitializeComponent();
            _positionsStack=new Stack<Model.Position>();
            Loaded += ClientsLoginWindow_Loaded;
            _voter=new Voter();
            //IgnoreTaskbarOnMaximize = true;
            BtnGo.Click += BtnGo_Click;
        }

        private async void ClientsLoginWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var election = await BlobCache.LocalMachine.GetObject<Settings>("ElectionSettings");
            VotingName.Content = election.ElectionName.ToUpper();
            VotingSubtitle.Content = election.EletionSubTitle.ToUpper();
            
            _positions = new List<Model.Position>();
            _positions = await BlobCache.LocalMachine.GetObject<IEnumerable<Model.Position>>("ElectionPositions");
            foreach (var position in _positions)
            {
                _positionsStack.Push(position);
            }
        }

        private void BtnGo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Pin.Text))
            {
              _voter=  ElectionConfigurationService.LoginVoter(new Voter {VoterCode = Pin.Text});
            }
            ConfirmVoterAsync();
           
        }

        public async void ConfirmVoterAsync()
        {
            if (_voter!=null)
            {
                if (!_voter.VoteInProgress && !_voter.Voted)
                {
                    new MainWindow(_positionsStack, _voter).Show();
                    Hide();
                }
                else
                {
                    var dialogSettings =new MetroDialogSettings {DialogMessageFontSize = 18, AffirmativeButtonText="Ok"};
                    await this.ShowMessageAsync("Error Confirming Voter","Please Contact Admin Your Details May Not Be Available\n Possible Cause: You May Have Already Voted",MessageDialogStyle.Affirmative,dialogSettings);
                }
            }
            
        }
    }
}
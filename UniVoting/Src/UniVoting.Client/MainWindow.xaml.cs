using System;
using System.Collections.Generic;
using System.Windows;
using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly Stack<Position> _positionsStack;
        private ClientVotingPage _votingPage;
        private Voter _voter;
        private List<Vote> _votes;

        public MainWindow(Stack<Position> positionsStack, Voter voter)
        {
            InitializeComponent();
            IgnoreTaskbarOnMaximize = true;
            _positionsStack = positionsStack;
            this._voter = voter;
            _votes=new List<Vote>();
            Loaded += MainWindow_Loaded;
            PageHolder.Navigated += PageHolder_Navigated;
            CandidateControl.VoteCast += CandidateControl_VoteCast;
        }

        private void CandidateControl_VoteCast(object source, EventArgs args)
        {
            if (_positionsStack.Count != 0)
            {
                PageHolder.Content = VotingPageMaker(_positionsStack);
            }
            else
            {
                _voter.Voted = true;
                VotingService.UpdateVoter(_voter);
                new ClientVoteCompletedPage(_votes).Show();
                Hide();
            }
        }
        private void PageHolder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            PageHolder.NavigationService.RemoveBackEntry();
        }

        private ClientVotingPage VotingPageMaker(Stack<Position> positions)
        {
            _votingPage = new ClientVotingPage(_voter, positions.Pop(),_votes);
           _votingPage.VoteCompleted += VotingPage_VoteCompleted;
            return _votingPage;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
         PageHolder.Content = VotingPageMaker(_positionsStack);
            _voter.VoteInProgress = true;
            VotingService.UpdateVoter(_voter);
        }

        private void VotingPage_VoteCompleted(object source, EventArgs args)
        {
            if (_positionsStack.Count != 0)
            {
               PageHolder.Content = VotingPageMaker(_positionsStack);
                
            }
            else
            {
                _voter.Voted=true;

                VotingService.UpdateVoter(_voter);
               new ClientVoteCompletedPage(_votes).Show();
                Hide();

            }
        }
    }
}

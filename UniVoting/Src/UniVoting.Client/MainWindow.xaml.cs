using System;
using System.Collections.Generic;
using System.Windows;
using MahApps.Metro.Controls;
using UniVoting.Model;
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

        public MainWindow(Stack<Position> positionsStack)
        {
            InitializeComponent();
            _positionsStack = positionsStack;
            Loaded += MainWindow_Loaded;
            PageHolder.Navigated += PageHolder_Navigated;
        }

        private void PageHolder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            PageHolder.NavigationService.RemoveBackEntry();
        }

        private ClientVotingPage VotingPageMaker(Stack<Position> positions)
        {
            _votingPage = new ClientVotingPage(new Voter(), positions.Pop());
           _votingPage.VoteCompleted += VotingPage_VoteCompleted;
            return _votingPage;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
         PageHolder.Content = VotingPageMaker(_positionsStack);
        }

        private void VotingPage_VoteCompleted(object source, EventArgs args)
        {
            if (_positionsStack.Count != 0)
            {
                
                  //  PageHolder.RemoveBackEntry();
                    PageHolder.Content = VotingPageMaker(_positionsStack);

            }
            else
            {
                PageHolder.Content = new ClientVoteCompletedPage();

            }
        }
    }
}

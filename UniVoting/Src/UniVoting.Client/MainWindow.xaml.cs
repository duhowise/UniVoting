using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using Akavache;
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
		private ConcurrentBag<Vote> _votes;
		private ConcurrentBag<SkippedVotes> _skippedVotes;
		public MainWindow(Stack<Position> positionsStack, Voter voter)
		{
			InitializeComponent();
			IgnoreTaskbarOnMaximize = true;
			_positionsStack = positionsStack;
			this._voter = voter;
			_skippedVotes = new ConcurrentBag<SkippedVotes>();
			_votes=new ConcurrentBag<Vote>();
			Loaded += MainWindow_Loaded;
			PageHolder.Navigated += PageHolder_Navigated;
			CandidateControl.VoteCast += CandidateControl_VoteCast;
            YesOrNoCandidateControl.VoteCast += YesOrNoCandidateControl_VoteCast;
            YesOrNoCandidateControl.VoteNo += YesOrNoCandidateControl_VoteNo		    ;
			Loaded += MainWindow_Loaded1;
		}

        private void YesOrNoCandidateControl_VoteNo(object source, EventArgs args)
        {
            ProcessVote();
        }

        private void YesOrNoCandidateControl_VoteCast(object source, EventArgs args)
        {
           ProcessVote();
        }

        private async void MainWindow_Loaded1(object sender, RoutedEventArgs e)
		{
			var election = await BlobCache.UserAccount.GetObject<Setting>("ElectionSettings");
		    MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) {Opacity = 0.2};
		}

		private void CandidateControl_VoteCast(object source, EventArgs args)
		{
		    ProcessVote();
		}

	    private void ProcessVote()
	    {
	        if (_positionsStack.Count != 0)
	        {
	            PageHolder.Content = VotingPageMaker(_positionsStack);
	        }
	        else
	        {
	            _voter.Voted = true;
	            new ClientVoteCompletedPage(_votes, _voter, _skippedVotes).Show();
	            Hide();
	        }
	    }

	    private void PageHolder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			PageHolder.NavigationService.RemoveBackEntry();
		}

		private ClientVotingPage VotingPageMaker(Stack<Position> positions)
		{
			_votingPage = new ClientVotingPage(_voter, positions.Pop(),_votes,_skippedVotes);
		   _votingPage.VoteCompleted += VotingPage_VoteCompleted;
			return _votingPage;
		}

	private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
		 PageHolder.Content = VotingPageMaker(_positionsStack);
			_voter.VoteInProgress = true;
			await VotingService.UpdateVoter(_voter);
		}

		private void VotingPage_VoteCompleted(object source, EventArgs args)
		{
			ProcessVote();
		}
	}
}

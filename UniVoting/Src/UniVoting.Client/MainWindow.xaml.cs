using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using Akavache;
using MahApps.Metro.Controls;
using UniVoting.Core;
using UniVoting.Services;
using Position = UniVoting.Core.Position;


namespace UniVoting.Client
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
	    AppDomain currentDomain = AppDomain.CurrentDomain;

        private readonly Stack<Position> _positionsStack;
		private ClientVotingPage _votingPage;
		private Voter _voter;
	    private readonly IVotingService _votingService;
	    private ConcurrentBag<Vote> _votes;
		private ConcurrentBag<SkippedVote> _skippedVotes;
		public MainWindow(Stack<Position> positionsStack, Voter voter,IVotingService votingService)
		{
			InitializeComponent();
			IgnoreTaskbarOnMaximize = true;
			_positionsStack = positionsStack;
			_voter = voter;
		    _votingService = votingService;
		    _skippedVotes = new ConcurrentBag<SkippedVote>();
			_votes=new ConcurrentBag<Vote>();
			Loaded += MainWindow_Loaded;
			PageHolder.Navigated += PageHolder_Navigated;
			CandidateControl.VoteCast += CandidateControl_VoteCast;
            YesOrNoCandidateControl.VoteCast += YesOrNoCandidateControl_VoteCast;
            YesOrNoCandidateControl.VoteNo += YesOrNoCandidateControl_VoteNo		    ;
			Loaded += MainWindow_Loaded1;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;


        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exp = e.ExceptionObject as Exception;
            MessageBox.Show(exp?.Message);
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
			var election = await BlobCache.UserAccount.GetObject<ElectionConfiguration>("ElectionSettings");
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
			await _votingService.UpdateVoter(_voter);
		}

		private void VotingPage_VoteCompleted(object source, EventArgs args)
		{
			ProcessVote();
		}
	}
}

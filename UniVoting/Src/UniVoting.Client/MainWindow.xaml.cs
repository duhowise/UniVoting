using System;
using System.Collections.Generic;
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
		private List<Vote> _votes;
		private List<SkippedVotes> _skippedVotes;
		public MainWindow(Stack<Position> positionsStack, Voter voter)
		{
			InitializeComponent();
			IgnoreTaskbarOnMaximize = true;
			_positionsStack = positionsStack;
			this._voter = voter;
			_skippedVotes = new List<SkippedVotes>();
			_votes=new List<Vote>();
			Loaded += MainWindow_Loaded;
			PageHolder.Navigated += PageHolder_Navigated;
			CandidateControl.VoteCast += CandidateControl_VoteCast;
			Loaded += MainWindow_Loaded1;
		}

		private async void MainWindow_Loaded1(object sender, RoutedEventArgs e)
		{
			var election = await BlobCache.LocalMachine.GetObject<Setting>("ElectionSettings");
			MainGrid.Background = new ImageBrush(Util.BitmapToImageSource(Util.ConvertBytesToImage(election.Logo)));
			MainGrid.Background.Opacity = 0.2;
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
				new ClientVoteCompletedPage(_votes, _voter,_skippedVotes).Show();
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
			   new ClientVoteCompletedPage(_votes,_voter,_skippedVotes).Show();
				Hide();

			}
		}
	}
}

using System;
using System.Collections.Concurrent;
using System.Windows;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;

namespace UniVoting.Client
{
	/// <summary>
	/// Interaction logic for ClientVoteCompletedPage.xaml
	/// </summary>
	public partial class ClientVoteCompletedPage : FluentWindow
	{
		private readonly ICacheService _cacheService;
		private readonly VotingService _votingService;
		private ConcurrentBag<Vote> _votes;
		private Voter _voter;
		private ConcurrentBag<SkippedVotes> _skippedVotes;

		public ClientVoteCompletedPage(ICacheService cacheService, VotingService votingService)
		{
			InitializeComponent();
			_cacheService = cacheService;
			_votingService = votingService;
		}

		public void Initialize(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVotes> skippedVotes)
		{
			_votes = votes;
			_voter = voter;
			_skippedVotes = skippedVotes;
			WindowState = WindowState.Maximized;
			Loaded += ClientVoteCompletedPage_Loaded;
		}

		private async void ClientVoteCompletedPage_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				await _votingService.CastVote(_votes, _voter,_skippedVotes);
				
				// Update the UI with vote counts - modify the existing TextBlock or add new UI elements
				Text.Text = $"Thank You For Voting\n\nVotes Cast: {_votes.Count}\nSkipped: {_skippedVotes.Count}\nVoter: {_voter.VoterName}";
				
				await _votingService.ResetVoter(_voter);
			}
			catch (Exception exception)
			{
				System.Windows.MessageBox.Show(exception.Message, "Vote Completion Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
			}
		}

		private async void BtnComplete_Click(object sender, RoutedEventArgs e)
		{
			await _cacheService.InvalidateAllAsync();
			var loginWindow = App.GetService<ClientsLoginWindow>();
			loginWindow.Show();
			Close();
		}
	}
}

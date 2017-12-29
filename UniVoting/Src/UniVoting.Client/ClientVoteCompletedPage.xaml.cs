using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Akavache;
using UniVoting.Model;
using UniVoting.Services;
using static System.Diagnostics.Process;

namespace UniVoting.Client
{
	/// <summary>
	/// Interaction logic for ClientVoteCompletedPage.xaml
	/// </summary>
	public partial class ClientVoteCompletedPage
	{
		private  List<Vote> _votes;
		private  Voter _voter;
		private List<SkippedVotes> _skippedVotes;

		private int count;
		public ClientVoteCompletedPage(List<Vote> votes,Voter voter, List<SkippedVotes> skippedVotes)
		{
			_votes = votes;
			_voter = voter;
			_skippedVotes = skippedVotes;
			InitializeComponent();
			IgnoreTaskbarOnMaximize = true;
			
			
			count = 0;
			Loaded += ClientVoteCompletedPage_Loaded;
		}

		private async void ClientVoteCompletedPage_Loaded(object sender, RoutedEventArgs e)
		{
			var election = await BlobCache.UserAccount.GetObject<Setting>("ElectionSettings");
			MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) { Opacity = 0.2 };
			try
			{
				await VotingService.CastVote(_votes, _voter,_skippedVotes);
				Text.Text = $"Good Bye {_voter.VoterName.ToUpper()}, Thank You For Voting";
			}
			catch (Exception)
			{
				Text.Text = $"Sorry An Error Occured.\nYour Votes Were not Submitted.\n Contact the Administrators";
				await VotingService.ResetVoter(_voter);

			}
			var _timer = new DispatcherTimer();
			_timer.Interval = new TimeSpan(0, 0, 0, 3);
			_timer.Tick += _timer_Tick;
			_timer.Start();
		}

		private  void _timer_Tick(object sender, EventArgs e)
		{
			count++;
			BlobCache.Shutdown().Wait();
			RestartApplication();
		}
		public void RestartApplication()
		{
			if (count == 1)
			{
				this.Hide();
				if (Application.ResourceAssembly != null) Start(Application.ResourceAssembly.Location);
				if (Application.Current != null)Application.Current.Shutdown();
				// OnRestartDue(this);

			}


		}

		
	}
}

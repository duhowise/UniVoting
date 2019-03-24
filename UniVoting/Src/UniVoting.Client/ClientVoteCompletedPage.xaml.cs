﻿using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Akavache;
using Autofac;
using UniVoting.Core;
using UniVoting.Services;
using BootStrapper = UniVoting.Client.Startup.BootStrapper;

namespace UniVoting.Client
{
	/// <summary>
	/// Interaction logic for ClientVoteCompletedPage.xaml
	/// </summary>
	public partial class ClientVoteCompletedPage
	{
		private ConcurrentBag<Vote> _votes;
		private  Voter _voter;
		private ConcurrentBag<SkippedVote> _skippedVotes;
        private IContainer container;
		private int _count;
		public ClientVoteCompletedPage(ConcurrentBag<Vote> votes,Voter voter, ConcurrentBag<SkippedVote> skippedVotes)
		{
			_votes = votes;
			_voter = voter;
			_skippedVotes = skippedVotes;
			InitializeComponent();
            container = new BootStrapper().BootStrap();
            IgnoreTaskbarOnMaximize = true;
			
			
			_count = 0;
			Loaded += ClientVoteCompletedPage_Loaded;
		}

		private async void ClientVoteCompletedPage_Loaded(object sender, RoutedEventArgs e)
		{
			var election = await BlobCache.UserAccount.GetObject<ElectionConfiguration>("ElectionSettings");
			MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) { Opacity = 0.2 };
			try
			{
				//make rabbitmq event here for submission of votes
				//submission of skipped votes
			    var service = container.Resolve<IVotingService>();
                await service.CastVote(_votes, _voter,_skippedVotes);
				Text.Text = $"Good Bye {_voter.VoterName.ToUpper()}, Thank You For Voting";
			}
			catch (Exception)
			{
				Text.Text = $"Sorry An Error Occured.\nYour Votes Were not Submitted.\n Contact the Administrators";

			    var service = container.Resolve<IVotingService>();
                await service.ResetVoter(_voter);

			}

            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 3)};
            timer.Tick += _timer_Tick;
			timer.Start();
		}

		private  void _timer_Tick(object sender, EventArgs e)
		{
			_count++;
			BlobCache.Shutdown().Wait();
			RestartApplication();
		}
		public void RestartApplication()
		{
            if (_count == 1)
            {
                //this.Hide();
                //Start(Application.ResourceAssembly.Location);
                //if (Application.Current != null) Application.Current.Shutdown();
                //// OnRestartDue(this);
                
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }


        }

		
	}
}

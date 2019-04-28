﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Akavache;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Core;
using UniVoting.Services;
using BootStrapper = UniVoting.Client.Startup.BootStrapper;
using IContainer = Autofac.IContainer;
using Position = UniVoting.Core.Position;

namespace UniVoting.Client
{
	/// <summary>
	/// Interaction logic for ClientsLoginWindow.xaml
	/// </summary>
	public partial class ClientsLoginWindow : MetroWindow
	{
		private readonly IElectionConfigurationService _electionConfigurationService;
		private IEnumerable<Position> _positions;
		 private Stack<Position> _positionsStack;
		private Voter _voter;
        private IContainer container;
		public ClientsLoginWindow()
		{
			InitializeComponent();
            container = new BootStrapper().BootStrap();

            _electionConfigurationService = container.Resolve<IElectionConfigurationService>();

            _positionsStack = new Stack<Position>();
			Loaded += ClientsLoginWindow_Loaded;
			_voter=new Voter();
			BtnGo.Click += BtnGo_Click;
		}
		
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true;
		}
		
		private async void ClientsLoginWindow_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
                //ThemeManagerHelper.CreateAppStyleBy(System.Windows.Media.Color.FromArgb(255, 122, 200, 122),true);
				var election = await BlobCache.UserAccount.GetObject<ElectionConfiguration>("ElectionSettings");
				MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) {Opacity = 0.2};
				VotingName.Text = election.ElectionName.ToUpper();
				VotingSubtitle.Content = election.ElectionSubTitle.ToUpper();

				_positions = new List<Position>();
				_positions = await BlobCache.UserAccount.GetObject<IEnumerable<Position>>("ElectionPositions");
				foreach (var position in _positions)
				{
					_positionsStack.Push(position);
				}
			}
			catch (Exception exception)
			{
			await this.ShowMessageAsync("Election Positions Error", exception.Message);
			}
		}

		private async void BtnGo_Click(object sender, RoutedEventArgs e)
		{
		   

			if (!string.IsNullOrWhiteSpace(Pin.Text))
			{
				try
				{
					_voter = await _electionConfigurationService.LoginVoterAsync(new Voter { VoterCode = Pin.Text });
				await	ConfirmVoterAsync(_voter);
				}
				catch (Exception exception)
				{
				await	this.ShowMessageAsync("Election Login Error", exception.Message);
					throw;
				}
			}
			
		   
		}

		public async Task ConfirmVoterAsync(Voter voter)
		{
		   
		    var votingservice = container.Resolve<IVotingService>();
            if (_voter!=null)
			{
				if (!_voter.VoteInProgress && !voter.Voted)
				{
					new MainWindow(_positionsStack, voter, votingservice).Show();
					Hide();
				}
				else
				{
					var dialogSettings =new MetroDialogSettings {DialogMessageFontSize = 18, AffirmativeButtonText="Ok"};
					await this.ShowMessageAsync("Error Confirming Voter","Please Contact Admin Your Details May Not Be Available\n Possible Cause: You May Have Already Voted",MessageDialogStyle.Affirmative,dialogSettings);
				Pin.Text=String.Empty;
				}
			}
			else
			{
				var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok" };
				await this.ShowMessageAsync("Error Confirming Voter", "Wrong Code!", MessageDialogStyle.Affirmative, dialogSettings);
				Pin.Text=String.Empty;
			}
			
		}
	}
}

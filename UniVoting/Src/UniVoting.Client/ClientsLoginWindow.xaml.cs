using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
			IgnoreTaskbarOnMaximize = true;
			BtnGo.IsDefault = true;
			BtnGo.Click += BtnGo_Click;
		}
		
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true;
		}
		
		private async void ClientsLoginWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			try
			{
				var election = await BlobCache.UserAccount.GetObject<Setting>("ElectionSettings");
				MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo));
				MainGrid.Background.Opacity = 0.2;
				VotingName.Content = election.ElectionName.ToUpper();
				VotingSubtitle.Content = election.EletionSubTitle.ToUpper();

				_positions = new List<Model.Position>();
				_positions = await BlobCache.UserAccount.GetObject<IEnumerable<Model.Position>>("ElectionPositions");
				foreach (var position in _positions)
				{
					_positionsStack.Push(position);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Election Positions Error");
			}
		}

		private async void BtnGo_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Pin.Text))
			{
				try
				{
					_voter = await ElectionConfigurationService.LoginVoter(new Voter { VoterCode = Pin.Text });
					ConfirmVoterAsync();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message, "Election Login Error");
					throw;
				}
			}
			
		   
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

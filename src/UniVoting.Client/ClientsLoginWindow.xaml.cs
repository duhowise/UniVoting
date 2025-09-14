using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace UniVoting.Client
{
	/// <summary>
	/// Interaction logic for ClientsLoginWindow.xaml
	/// </summary>
	public partial class ClientsLoginWindow : FluentWindow
	{
		private readonly ICacheService _cacheService;
		private IEnumerable<Model.Position> _positions;
		 private Stack<Model.Position> _positionsStack;
		private Voter _voter;
		
		public ClientsLoginWindow(ICacheService cacheService)
		{
			_cacheService = cacheService;
			InitializeComponent();
			_positionsStack=new Stack<Model.Position>();
			Loaded += ClientsLoginWindow_Loaded;
			_voter=new Voter();
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
                //ThemeManagerHelper.CreateAppStyleBy(System.Windows.Media.Color.FromArgb(255, 122, 200, 122),true);
				var election = await _cacheService.GetObjectAsync<Setting>("ElectionSettings");
				MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) {Opacity = 0.2};
				VotingName.Text = election.ElectionName.ToUpper();
				VotingSubtitle.Content = election.EletionSubTitle.ToUpper();

				_positions = new List<Model.Position>();
				_positions = await _cacheService.GetObjectAsync<IEnumerable<Model.Position>>("ElectionPositions");
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
					// Note: ElectionConfigurationService should be injected as instance service
					// _voter = await _electionConfigurationService.LoginVoter(new Voter { VoterCode = Pin.Text });
					// For now, commenting out the static call
					// _voter = await ElectionConfigurationService.LoginVoter(new Voter { VoterCode = Pin.Text });
					// ConfirmVoterAsync();
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
					var mainWindow = App.GetService<MainWindow>();
					mainWindow.Initialize(_positionsStack, _voter);
					mainWindow.Show();
					Hide();
				}
				else
				{
					var dialog = new ContentDialog
					{
						Title = "Error Confirming Voter",
						Content = "Please Contact Admin Your Details May Not Be Available\n Possible Cause: You May Have Already Voted",
						PrimaryButtonText = "Ok"
					};
					await dialog.ShowAsync();
				Pin.Text=String.Empty;
				}
			}
			else
			{
				var dialog = new ContentDialog
				{
					Title = "Error Confirming Voter",
					Content = "Wrong Code!",
					PrimaryButtonText = "Ok"
				};
				await dialog.ShowAsync();
				Pin.Text=String.Empty;
			}
			
		}
	}
}

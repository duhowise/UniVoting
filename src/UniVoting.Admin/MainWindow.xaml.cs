using Microsoft.Extensions.Logging;
using System.ComponentModel;
using UniVoting.Admin.Administrators;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : FluentWindow
	{
		private readonly VotingService _votingService;
		private readonly ElectionConfigurationService _electionConfigurationService;
		private readonly ILoggerFactory _loggerFactory;

        public MainWindow(VotingService votingService, ElectionConfigurationService electionConfigurationService, ILoggerFactory loggerFactory)
		{
			InitializeComponent();
			_votingService = votingService;
			_electionConfigurationService = electionConfigurationService;
			_loggerFactory = loggerFactory;
			
			// Apply WPF UI theme
			Wpf.Ui.Appearance.ApplicationThemeManager.Apply(this);
		}

		public void Initialize(Commissioner comissioner)
		{
			PageHolder.Content = new AdminMenuPage(comissioner, _votingService, _electionConfigurationService, _loggerFactory);
		}

        protected override void OnClosing(CancelEventArgs e)
		{
			var loginWindow = App.GetService<AdminLoginWindow>();
			loginWindow.Show();
			base.OnClosing(e);
		}
    }
}

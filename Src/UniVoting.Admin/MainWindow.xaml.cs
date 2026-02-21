using System;
using Avalonia.Controls;
using UniVoting.Admin.Administrators;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin
{
    public partial class MainWindow : Window
    {
        public static Action<object?>? Navigate;

        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly ILogger _logger;

        public MainWindow()
        {
            InitializeComponent();
            _electionService = null!;
            _votingService = null!;
            _logger = null!;
        }

        public MainWindow(Comissioner comissioner, IElectionConfigurationService electionService, IVotingService votingService, ILogger logger)
        {
            _electionService = electionService;
            _votingService = votingService;
            _logger = logger;
            InitializeComponent();
            Navigate = (content) => PageHolder.Content = content;
            PageHolder.Content = new AdminMenuPage(comissioner, _electionService, _votingService);
        }

        protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
        {
            new AdminLoginWindow(_electionService, _votingService, _logger).Show();
            base.OnClosing(e);
        }
    }
}

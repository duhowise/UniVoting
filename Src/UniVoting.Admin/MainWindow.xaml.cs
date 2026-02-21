using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public MainWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public MainWindow(IAdminSessionService session, IElectionConfigurationService electionService, IVotingService votingService, ILogger logger, IServiceProvider sp)
        {
            _electionService = electionService;
            _votingService = votingService;
            _logger = logger;
            _sp = sp;
            InitializeComponent();
            Navigate = (content) => PageHolder.Content = content;
            PageHolder.Content = _sp.GetRequiredService<AdminMenuPage>();
        }

        protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
        {
            _sp.GetRequiredService<AdminLoginWindow>().Show();
            base.OnClosing(e);
        }
    }
}

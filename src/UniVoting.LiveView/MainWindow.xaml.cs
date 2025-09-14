using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UniVoting.Services;
using Wpf.Ui.Controls;
using Position = UniVoting.Model.Position;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
	{
		IEnumerable<Position> _positions;
	    readonly ILogger<MainWindow> _logger;
	    private readonly LiveViewService _liveViewService;

        public MainWindow(LiveViewService liveViewService, ILogger<MainWindow> logger)
		{
			InitializeComponent();
			ExtendsContentIntoTitleBar = true;
            // Apply WPF UI theme
            Wpf.Ui.Appearance.ApplicationThemeManager.Apply(this);
			
			_liveViewService = liveViewService;
			_positions=new List<Position>();
            _logger = logger;
            Loaded += MainWindow_Loaded;
		}
	
		private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
		    try
		    {
		        _positions = await _liveViewService.Positions();

		    }
		    catch (MySqlException exception)
		    {
		        _logger.LogError(exception, "MySQL exception occurred while loading positions");

		    }
		    catch (Exception exception)
		    {
		        _logger.LogError(exception, "General exception occurred while loading positions");

		    }
		    finally
		    {
		        if (_positions != null)
		        {
		            foreach (var position in _positions)
		            {
		                CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName, App.GetService<LiveViewService>(), App.GetService<ILogger<TileControlLarge>>()));
		                SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName, App.GetService<LiveViewService>(), App.GetService<ILogger<TileControlSmall>>()));
		            }
                }
            }
		}
	}
}

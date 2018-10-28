using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Autofac;
using MahApps.Metro.Controls;
using UniVoting.LiveView.Startup;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
	{
	    private readonly ILiveViewService _liveViewService;
	    IEnumerable<Position> _positions;
	    readonly ILogger _logger;

        public MainWindow()
		{
		    InitializeComponent();

		    var container = new BootStrapper().BootStrap();
		    _liveViewService = container.Resolve<ILiveViewService>();
			_positions = new List<Position>();
            _logger = new SystemEventLoggerService();
            Loaded += MainWindow_Loaded;
		   
		}
	
		private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
		    try
		    {
		        _positions = await _liveViewService.Positions();

		    }
		    catch (SqlException exception)
		    {
		        SystemEventLoggerService.Log(exception.StackTrace);

		    }
		    catch (Exception exception)
		    {
		        _logger.Log(exception);

		    }
		    finally
		    {
		        foreach (var position in _positions)
		        {
		            CastedVotesHolder.Children.Add(new TileControlLarge(position, _liveViewService));
		            SkippedVotesHolder.Children.Add(new TileControlSmall(position,_liveViewService));
		        }
            }
            
		}
	}
}

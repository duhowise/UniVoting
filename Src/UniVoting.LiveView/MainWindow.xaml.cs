using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MahApps.Metro.Controls;
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
		IEnumerable<Position> _positions;
	    readonly ILogger _logger;

        public MainWindow()
		{
			InitializeComponent();
			_positions=new List<Position>();
            _logger = new SystemEventLoggerService();
            Loaded += MainWindow_Loaded;
		   
		}
	
		private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
		    try
		    {
		        _positions = await LiveViewService.Positions();

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
		            CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName?.Trim()));
		            SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName?.Trim()));
		        }
            }
            
		}
	}
}

using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for TileControlLarge.xaml
    /// </summary>
    public partial class TileControlLarge : UserControl
    {
        private DispatcherTimer _timer;
        private string _position;
        public TileControlLarge(String position)
        {
            InitializeComponent();
            _timer=new DispatcherTimer();
            _timer.Interval=new TimeSpan(0,0,0,1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _position = position;
            Position.Text = _position.ToUpper();

        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await LiveViewService.VoteCountAsync(_position)}";
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
               // throw;
            }
            finally
            {
                VoteCount.Text = $"{await LiveViewService.VoteCountAsync(_position)}";
            }
        }
    }
}

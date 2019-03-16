using System.Windows.Controls;
using Autofac;
using Univoting.Services;

namespace UniVoting.Admin.Administrators
{
    /// <summary>
    /// Interaction logic for AddPositionDialogControl.xaml
    /// </summary>
    public partial class AddPositionDialogControl : UserControl
    {

        public AddPositionDialogControl()
        {
            Loaded += AddPositionDialogControl_Loaded;
            InitializeComponent();
        }

        private void AddPositionDialogControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
             var electionConfigurationService=new Startup.BootStrapper().BootStrap().Resolve<IElectionConfigurationService>();

        Faculty.ItemsSource =  electionConfigurationService.GetAllPositionsAsync().Result;

        }
    }
}

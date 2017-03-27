using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for AdminMenuPage.xaml
    /// </summary>
    public partial class AdminMenuPage : Page
    {
        public AdminMenuPage()
        {
            InitializeComponent();
        }

        private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
              NavigationService?.Navigate(new AdminCreateAccountPage());
            
        }

        private void BtnSetUpElection_Click(object sender, RoutedEventArgs e)
        {
            
                NavigationService?.Navigate(new AdminSetUpElectionPage());
            
        }

        private void BtnSetUpPostions_Click(object sender, RoutedEventArgs e)
        {
            
                NavigationService?.Navigate(new AdminSetUpPositionPage());
            
        }

        private void BtnSetUpCandidates_Click(object sender, RoutedEventArgs e)
        {
            
                NavigationService?.Navigate(new AdminSetUpCandidatesPage());
            
        }
    }
}

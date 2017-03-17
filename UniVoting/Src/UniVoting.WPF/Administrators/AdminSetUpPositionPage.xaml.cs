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
    /// Interaction logic for AdminSetUpPositionPage.xaml
    /// </summary>
    public partial class AdminSetUpPositionPage : Page
    {
        public static AdminSetUpPositionPage Instance = null;
        public AdminSetUpPositionPage()
        {
            InitializeComponent();
            Instance = this;
            /*async load all positons from db 
            *foreach postion in results
            * 
            *set values on the PositionControl
            *var pc = new PositionControl();
            *pc.ID = databaseValue;
            *pc.Position = databaseValue;

            *PositionControlHolder.Children.Add(pc);
            */
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            PositionControlHolder.Children.Add(new PositionControl());
        }

        public void RemovePosition(UserControl c)
        {
            PositionControlHolder.Children.Remove(c);
        }
    }
}

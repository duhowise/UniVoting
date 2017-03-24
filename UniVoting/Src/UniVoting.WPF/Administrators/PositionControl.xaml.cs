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
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for PositionControl.xaml
    /// </summary>
    public partial class PositionControl : UserControl
    {
        private int id;
        private string position;
        //id of the position in database
        public int ID { get { return id; } set { id = value; } }
        //name of the position in database
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                TextBoxPosition.Text = position;
            }
        }

        public PositionControl()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
           new ElectionService().AddPosition(new Position
           {
               
           });


        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //set TextBoxPosition IsEnabled="true" after updating set TextBoxPosition IsEnabled="False"
            new ElectionService().UpdatePosition(new Position
            {
                
            });
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs a)
        {
            var response = System.Windows.MessageBox.Show("Are You Sure You Want to DELETE Position", "Delete",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (response == MessageBoxResult.Yes)
            {
               AdminSetUpPositionPage.Instance.RemovePosition(this);
                new ElectionService().RemovePosition(new Position
                {
                    
                });
            }
        }
    }
}

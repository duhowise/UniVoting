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
using Microsoft.Win32;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for AdminSetUpElectionPage.xaml
    /// </summary>
    public partial class AdminSetUpElectionPage : Page
    {
        public AdminSetUpElectionPage()
        {
            InitializeComponent();
            BtnUploadImage.Click += BtnUploadImage_Click;
        }

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            var op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
            {
               var image=new BitmapImage(new Uri(op.FileName));
                System.Drawing.Image converted = Util.ConvertImage(image);
               Logo = Util.ResizeImage(converted,300, 300);
            }
        }
    }
}

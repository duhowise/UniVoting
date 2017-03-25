using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UniVoting.Model;
using UniVoting.Services;
using Color = System.Windows.Media.Color;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for AdminSetUpElectionPage.xaml
    /// </summary>
    public partial class AdminSetUpElectionPage : Page
    {
        private System.Windows.Media.Color _chosencolor;
        public AdminSetUpElectionPage()
        {
            InitializeComponent();
            BtnUploadImage.Click += BtnUploadImage_Click;
            Loaded += AdminSetUpElectionPage_Loaded;
            Colorbox.GotFocus += Colorbox_GotFocus;
            SaveElection.Click += SaveElection_Click;                                             
        }
        private void Colorbox_GotFocus(object sender, RoutedEventArgs e)
        {
            using (var color = new ColorDialog())
            {
                if (color.ShowDialog() != DialogResult.None)
                {
                    Colorbox.Text = color.Color.Name;
                    _chosencolor = System.Windows.Media.Color.FromRgb(color.Color.R, color.Color.G, color.Color.B);
                    ColoView.Fill = new SolidColorBrush(_chosencolor);
                }
            }
        }
        private void SaveElection_Click(object sender, RoutedEventArgs e)
        {
            if (
                !string.IsNullOrWhiteSpace(TextBoxElectionName.Text)||!string.IsNullOrWhiteSpace(TextBoxElectionName.Text))
            {
                new ElectionService().NewElection(new Settings
                {
                    ElectionName = TextBoxElectionName.Text,
                    EletionSubTitle = TextBoxSubtitle.Text,
                    Colour = string.Join(",", _chosencolor.R.ToString(),_chosencolor.G.ToString(),_chosencolor.B.ToString()),
                    Logo = Util.ConvertToBytes(Logo)
                    
                });
                Util.Clear(this);
            }
        }

        



        private void AdminSetUpElectionPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Colorbox.ItemsSource = Enum.GetNames(typeof(System.Drawing.KnownColor));
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
              var final = Util.ResizeImage(converted, 300, 300);
                Logo.Source = Util.BitmapToImageSource(final);
            }
        }
    }
}

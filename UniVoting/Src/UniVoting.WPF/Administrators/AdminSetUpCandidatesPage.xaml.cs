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
    /// Interaction logic for AdminSetUpCandidatesPage.xaml
    /// </summary>
    public partial class AdminSetUpCandidatesPage : Page
    {
        public List<Item> Items { get; set; }
        public AdminSetUpCandidatesPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Items = new List<Item>();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                Item ItemToAdd = new Item();
                ItemToAdd.Name = _generateRandomString(r.Next(10, 20), r);
                ItemToAdd.PictureID = r.Next(0, 2);

                Items.Add(ItemToAdd);
            }

        
            this.CandidatesList.DataContext = this;
        }

        private string _generateRandomString(int length, Random r)
        {
            String ReturnString = "";
            for (int idx = 0; idx < length; idx++)
                ReturnString += char.ConvertFromUtf32(65 + r.Next(0, 23));
            return ReturnString;
        }

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                CandidateImage.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}

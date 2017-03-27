using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
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
using UniVoting.Model;
using UniVoting.Services;
using Image = System.Windows.Controls.Image;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for AdminSetUpCandidatesPage.xaml
    /// </summary>
    public partial class AdminSetUpCandidatesPage : Page
    {
        public ObservableCollection<CandidateDto> Candidates { get; set; }

        public  class CandidateDto
        {
            public  int Id { get; set; }
            public  int? PositionId { get; set; }
            public  string CandidateName { get; set; }
            public Bitmap CandidatePicture { get; set; }
            public  int? RankId { get; set; }
            
        }

        private List<int> _rank;
        public AdminSetUpCandidatesPage()
        {
            InitializeComponent();
            _rank = new List<int>();
            for (int i = 1; i <= 10; ++i)
            {
                _rank.Add(i);
            }
            RankCombo.ItemsSource = _rank;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            PositionCombo.ItemsSource = ElectionService.GetAllPositions();
            //Items = new List<Item>();
            //Random r = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    Item ItemToAdd = new Item();
            //    ItemToAdd.Name = _generateRandomString(r.Next(10, 20), r);
            //    ItemToAdd.PictureID = r.Next(0, 2);

            //    Items.Add(ItemToAdd);
            //}

            var candidates = new ObservableCollection<Candidate>(ElectionService.GetAllCandidates().ToList());
            foreach (var candidate in candidates)
            {
                
                var newcandidate=new CandidateDto
               {
                   CandidateName = candidate.CandidateName,
                   CandidatePicture =Util.ConvertBytesToImage(candidate.CandidatePicture),
                   Id = candidate.Id,
                   PositionId = candidate.PositionId,
                   RankId = candidate.RankId


                };
                 
            }
            this.CandidatesList.DataContext = this;
        }

        //private string _generateRandomString(int length, Random r)
        //{
        //    String ReturnString = "";
        //    for (int idx = 0; idx < length; idx++)
        //        ReturnString += char.ConvertFromUtf32(65 + r.Next(0, 23));
        //    return ReturnString;
        //}

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
                var image = new BitmapImage(new Uri(op.FileName));
                System.Drawing.Image converted = Util.ConvertImage(image);
                var final = Util.ResizeImage(converted, 300, 300);
                CandidateImage.Source = Util.BitmapToImageSource(final);
            }
        }
    }
}

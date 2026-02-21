using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UniVoting.Model;
using UniVoting.Services;
using Microsoft.Win32;

namespace UniVoting.Admin.Administrators
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
			// Color is entered as comma-separated RGB (e.g. "255,128,0")
			var colorText = Colorbox.Text.Trim();
			if (colorText.Contains(","))
			{
				var parts = colorText.Split(',');
				if (parts.Length == 3 &&
					byte.TryParse(parts[0], out byte r) &&
					byte.TryParse(parts[1], out byte g) &&
					byte.TryParse(parts[2], out byte b))
				{
					_chosencolor = System.Windows.Media.Color.FromRgb(r, g, b);
					ColoView.Fill = new SolidColorBrush(_chosencolor);
				}
			}
		}
		private async void SaveElection_Click(object sender, RoutedEventArgs e)
		{
			if (
				!string.IsNullOrWhiteSpace(TextBoxElectionName.Text)||!string.IsNullOrWhiteSpace(TextBoxElectionName.Text))
			{
				await ElectionConfigurationService.NewElection(new Setting
				{
					ElectionName = TextBoxElectionName.Text,
					EletionSubTitle = TextBoxSubtitle.Text,
					Colour = string.Join(",", _chosencolor.R.ToString(), _chosencolor.G.ToString(), _chosencolor.B.ToString()),
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

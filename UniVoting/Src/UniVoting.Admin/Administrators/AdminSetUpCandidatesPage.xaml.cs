using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for AdminSetUpCandidatesPage.xaml
	/// </summary>
	public partial class AdminSetUpCandidatesPage : Page
	{
		public  ObservableCollection<CandidateDto> Candidates =new ObservableCollection<CandidateDto>();
		public  class CandidateDto
		{
			public  int Id { get; set; }
			public  int? PositionId { get; set; }
			public  string CandidateName { get; set; }
			public BitmapImage CandidatePicture { get; set; }
			public  int? RankId { get; set; }
			public string Position { get; set; }
			public string Rank => $"Rank: {RankId}";


		}
		private List<int> _rank;
		private int _candidateId;
		public AdminSetUpCandidatesPage()
		{
			this._candidateId = 0;
			InitializeComponent();
			SaveCandidate.Click += SaveCandidate_Click;
			#region RankGeneration
			_rank = new List<int>();
			for (int i = 1; i <= 10; ++i)
			{
				_rank.Add(i);
			}
			RankCombo.ItemsSource = _rank; 
			#endregion
		}

		private async void SaveCandidate_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(CandidateName.Text)||!string.IsNullOrWhiteSpace(PositionCombo.Text)
				||!string.IsNullOrWhiteSpace(RankCombo.Text)||!string.IsNullOrWhiteSpace(RankCombo.Text)
				||CandidateImage!=null
			   
				)
			{
				var candidate = new Candidate
				{
					Id = _candidateId,
					CandidateName = CandidateName.Text,
					CandidatePicture = Util.ConvertToBytes(CandidateImage),
					PositionId = (int) PositionCombo.SelectedValue,
					RankId = (int) RankCombo.SelectedValue
				};
				 await ElectionConfigurationService.SaveCandidate(candidate);
				Util.Clear(this);
				CandidateImage.Source=new BitmapImage(new Uri("../Resources/images/people_on_the_beach_300x300.jpg", UriKind.Relative));
				PositionCombo.ItemsSource = await ElectionConfigurationService.GetAllPositionsAsync();
				RefreshCandidateList();


			}

		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			PositionCombo.ItemsSource = await ElectionConfigurationService.GetAllPositionsAsync();
			RefreshCandidateList();
		}

		private async void RefreshCandidateList()
		{
			var candidates = await new ElectionConfigurationService().GetAllCandidates();
			foreach (var candidate in candidates)
			{
				var newcandidate = new CandidateDto
				{
					CandidateName = candidate.CandidateName,
					CandidatePicture = Util.ByteToImageSource(candidate.CandidatePicture),
					Id = candidate.Id,
					PositionId = candidate.PositionId,
					RankId = candidate.RankId,
					Position =candidate.Position.PositionName ?? String.Empty
				};
				Candidates.Add(newcandidate);
			}

			CandidatesList.ItemsSource = Candidates;
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
				var image = new BitmapImage(new Uri(op.FileName));
				System.Drawing.Image converted = Util.ConvertImage(image);
				var final = Util.ResizeImage(converted, 300, 300);
				CandidateImage.Source = Util.BitmapToImageSource(final);
			}
		}

		private void CandidatesList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var editCandidate = CandidatesList.SelectedItem as CandidateDto ?? null;
			if (editCandidate!=null)
			{
				_candidateId = editCandidate.Id;
				CandidateName.Text = editCandidate.CandidateName;
				CandidateImage.Source = editCandidate.CandidatePicture;
				PositionCombo.SelectedValue = editCandidate.PositionId;
				RankCombo.SelectedValue = editCandidate.RankId;
			}

		}
	}
}

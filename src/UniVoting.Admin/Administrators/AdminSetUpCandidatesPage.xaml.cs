using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for AdminSetUpCandidatesPage.xaml
    /// </summary>

    internal class CandidateDto
    {
        public CandidateDto()
        {
            Id = 0;
        }
        public CandidateDto(int id, int positionid, string candidateName, BitmapImage candidatepicture, int rankId, string position)
        {
            Id = id;
            PositionId = positionid;
            CandidateName = candidateName;
            CandidatePicture = candidatepicture;
            RankId = rankId;
            Position = position;
        }
        public int Id { get; set; }
        public int? PositionId { get; set; }
        public string CandidateName { get; set; }
        public BitmapImage CandidatePicture { get; set; }
        public int? RankId { get; set; }
        public string Position { get; set; }
        public string Rank => $"Rank: {RankId}";


    }

    public partial class AdminSetUpCandidatesPage : Page
	{
        private readonly ILogger<AdminSetUpCandidatesPage> _logger;
        private readonly ElectionConfigurationService _electionConfigurationService;
		
		internal  ObservableCollection<CandidateDto> Candidates =new ObservableCollection<CandidateDto>();
		
		private List<int> _rank;
		private int _candidateId;
		public AdminSetUpCandidatesPage(ElectionConfigurationService electionConfigurationService, ILogger<AdminSetUpCandidatesPage> logger)
		{
			this._candidateId = 0;
			InitializeComponent();
			_electionConfigurationService = electionConfigurationService;
			_logger = logger;
			
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
			if (!string.IsNullOrWhiteSpace(CandidateName.Text)|!string.IsNullOrWhiteSpace(PositionCombo.Text)
				|!string.IsNullOrWhiteSpace(RankCombo.Text)|!string.IsNullOrWhiteSpace(RankCombo.Text)
				||CandidateImage!=null
			   
				)
			{
				var candidate = new Candidate();
			    candidate.Id = _candidateId;
			    candidate.CandidateName = CandidateName.Text;
			    candidate.CandidatePicture = Util.ConvertToBytes(CandidateImage);
			    candidate.PositionId = (int) PositionCombo.SelectedValue;
			    candidate.RankId = (int) RankCombo.SelectedValue;
			    await _electionConfigurationService.SaveCandidate(candidate);
				Util.Clear(this);
				CandidateImage.Source=new BitmapImage(new Uri("../Resources/images/people_on_the_beach_300x300.jpg", UriKind.Relative));
			await	RefreshCandidateList();


			}

		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{

			await	RefreshCandidateList();
		    PositionCombo.ItemsSource = await _electionConfigurationService.GetAllPositionsAsync();


        }

        private async Task RefreshCandidateList()
		{
            Candidates.Clear();
		    try
		    {
		       var candidates = await _electionConfigurationService.GetCandidateWithDetails();
		        if ( candidates!=null)
		        {
		            foreach (var candidate in  candidates)
		            {
		                var newcandidate = new CandidateDto(candidate.Id, Convert.ToInt32(candidate.PositionId)
		                    , candidate.CandidateName, Util.ByteToImageSource(candidate.CandidatePicture),
		                    Convert.ToInt32(candidate.RankId)
		                    , candidate.Position.PositionName);
		                Candidates.Add(newcandidate);
		            }
                }
                
		    }
		    catch (Exception e)
		    {
		      _logger.LogError(e, "Failed to refresh candidate list");
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
				var converted = Util.ConvertImage(image);
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

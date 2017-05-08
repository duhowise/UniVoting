using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
	/// <summary>
	/// Interaction logic for CandidateControl.xaml
	/// </summary>
	public partial class CandidateControl : UserControl
	{


		public int CandidateId
		{
			get { return (int)GetValue(CandidateIdProperty); }
			set { SetValue(CandidateIdProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CandidateId.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CandidateIdProperty =
			DependencyProperty.Register("CandidateId", typeof(int), typeof(CandidateControl), new PropertyMetadata(0));
		public delegate void VoteCastEventHandler(object source, EventArgs args);

		public static event VoteCastEventHandler VoteCast;

		private List<Vote> _votes;
		private Position _position;
		private Candidate _candidate;
		private Voter _voter;
		public CandidateControl(List<Vote> votes,Position position, Candidate candidate, Voter voter)
		{
			InitializeComponent();
			this._votes = votes;
			this._position = position;
			this._candidate = candidate;
			this._voter = voter;
			Loaded += CandidateControl_Loaded;
			BtnVote.Click += BtnVote_Click;
		}
		
		private void CandidateControl_Loaded(object sender, RoutedEventArgs e)
		{
			CandidateId = _candidate.Id;
			CandidateName.Content = _candidate.CandidateName.ToUpper();
			CandidateImage.Source =Util.ByteToImageSource(_candidate.CandidatePicture);
			Rank.Content =$"#{_candidate.RankId}" ;
		}

		private async void BtnVote_Click(object sender, RoutedEventArgs e)
		{
			var metroWindow = (Window.GetWindow(this) as MetroWindow);
			var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok", };

			MessageDialogResult result = await metroWindow.ShowMessageAsync("Cast Vote", $"Are You Sure You Want to Vote For {_candidate.CandidateName} ?", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
			if (result==MessageDialogResult.Affirmative)
			{
				_votes.Add(new Vote {CandidateId = CandidateId,PositionId =_position.Id,VoterId =_voter.Id });
				OnVoteCast(this);
			}
		}

		private static void OnVoteCast(object source)
		{
			VoteCast?.Invoke(source, EventArgs.Empty);  
		}
	}
}

using System;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using Position = UniVoting.Core.Position;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	///     Interaction logic for AdminSetUpPositionPage.xaml
	/// </summary>
	public partial class AdminSetUpPositionPage
	{
		private IElectionConfigurationService _electionConfigurationService;
        private IContainer container;
        private int _positionId;
        public Position CurrentPosition { get; set; }
       
        public AdminSetUpPositionPage()
		{
            
			InitializeComponent();
            _positionId = 0;
             container = new BootStrapper().BootStrap();
            Loaded += AdminSetUpPositionPage_Loaded;
            AddPosition.Click += AddPosition_Click;
            PositionList.MouseDoubleClick += PositionList_MouseDoubleClick;
            
        }

        private void PositionList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PositionList.SelectedItem is Position pos)
            {
                CurrentPosition = pos;
                _positionId = CurrentPosition.Id;
                TextBoxPositionName.Text = CurrentPosition.PositionName;
                FacultyList.SelectedValue=CurrentPosition.FacultyId;
            }
        }

       
        private async void AddPosition_Click(object sender, RoutedEventArgs e)
        {
            
            var settings = new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Theme,
                AnimateShow = true,
            };

            if (string.IsNullOrWhiteSpace(TextBoxPositionName.Text)||string.IsNullOrWhiteSpace(FacultyList.Text))
            {
            await this.ShowMessageAsync("Add new Position", "Please Specify Position Name ");

            }
            var result = await this.ShowMessageAsync("Add new Position", "are you sure you want to add ", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result != MessageDialogResult.Affirmative) return;
            CurrentPosition = new Position { Id = _positionId, PositionName = TextBoxPositionName.Text,RankId = Convert.ToInt32(RankList.SelectedValue), FacultyId = Convert.ToInt32(FacultyList.SelectedValue) };
            _electionConfigurationService = container.Resolve<IElectionConfigurationService>();
            await _electionConfigurationService.AddPositionAsync(CurrentPosition);
            await this.ShowMessageAsync("Add new Position", "success");
            AdminSetUpPositionPage_Loaded(this, e);

        }

        private async void AdminSetUpPositionPage_Loaded(object sender, RoutedEventArgs e)
        {
            _electionConfigurationService =container.Resolve<IElectionConfigurationService>();
            FacultyList.ItemsSource =await  _electionConfigurationService.GetFacultiesAsync();
            PositionList.ItemsSource
                = await  _electionConfigurationService.GetAllPositionsAsync(true);

            RankList.ItemsSource
                = await  _electionConfigurationService.GetAllRanksAsync();

        }
        
    }
}

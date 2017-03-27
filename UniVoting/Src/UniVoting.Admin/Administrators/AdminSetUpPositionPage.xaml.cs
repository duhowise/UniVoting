using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    ///     Interaction logic for AdminSetUpPositionPage.xaml
    /// </summary>
    public partial class AdminSetUpPositionPage : Page
    {
        public static AdminSetUpPositionPage Instance;

        public AdminSetUpPositionPage()
        {
            InitializeComponent();
           Instance = this;
            //Loaded += AdminSetUpPositionPage_Loaded;
            Instance.Loaded += Instance_Loaded;
            /*async load all positons from db 
            *foreach postion in results
            * 
            *set values on the PositionControl
            *var pc = new PositionControl();
            *pc.ID = databaseValue;
            *pc.Position = databaseValue;

            *PositionControlHolder.Children.Add(pc);
            */
        }

        private void Instance_Loaded(object sender, RoutedEventArgs e)
        {
            PositionControlHolder.Children.Clear();
           var positions = ElectionConfigurationService.GetAllPositions();
            foreach (var position in positions)
                PositionControlHolder.Children.Add(new PositionControl
                {
                    TextBoxPosition = {Text = position.PositionName},
                    Id = position.Id
                });
        }


        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = Window.GetWindow(this) as MetroWindow;
            var settings = new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Accented,
                AffirmativeButtonText = "OK",
                AnimateShow = true,
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel"
            };
            var result = await metroWindow.ShowInputAsync("Enter New Position ", "Position Name", settings);
            PositionControlHolder.Children.Add(new PositionControl(result));
        }

        public void RemovePosition(UserControl c)
        {
            PositionControlHolder.Children.Remove(c);
        }
    }
}

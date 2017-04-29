using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for PositionControl.xaml
    /// </summary>
    public partial class PositionControl : UserControl
    {


        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(PositionControl), new PropertyMetadata(0));


        //private Position _position;
        //public static Position PositionValue { get; set; }
        public PositionControl(string name)
        {
            InitializeComponent();
            TextBoxPosition.Text=name;
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
              var value= ElectionConfigurationService.AddPosition(new Position { PositionName = TextBoxPosition.Text });
                Id = value.Id;
            }
        }

        public PositionControl()
        {
            InitializeComponent();
            
        }
        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Window.GetWindow(this) as MetroWindow);
            var settings = new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Accented,
                AffirmativeButtonText = "OK",
                AnimateShow = true,
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel",

            };
            var data = await metroWindow.ShowInputAsync("Edit Position Position ", $"Previous Name:{TextBoxPosition.Text} ", settings);
            if (data != string.Empty) TextBoxPosition.Text = data;
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                
                //var value = ElectionService.GetPosition(_position);
              await ElectionConfigurationService.UpdatePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
            }

            //set TextBoxPosition IsEnabled = "true" after updating set TextBoxPosition IsEnabled = "False"

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs a)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                var response = System.Windows.MessageBox.Show("Are You Sure You Want to DELETE Position", "Delete",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (response == MessageBoxResult.Yes)
                {
                    AdminSetUpPositionPage.Instance.RemovePosition(this);
                  ElectionConfigurationService.RemovePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
                }
            }
            
        }
    }
}

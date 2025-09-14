using System.Windows;
using System.Windows.Controls;
using UniVoting.Services;
using Wpf.Ui.Controls;
using Position = UniVoting.Model.Position;

namespace UniVoting.Admin.Administrators
{
    /// <summary>
    /// Interaction logic for PositionControl.xaml
    /// </summary>
    public partial class PositionControl : UserControl
    {
        private ContentDialog _contentDialog;
        private AddPositionDialogControl _addPositionDialogControl;
        private readonly ElectionConfigurationService _electionConfigurationService;

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
        public PositionControl(ElectionConfigurationService electionConfigurationService, string name)
        {
            InitializeComponent();
            _electionConfigurationService = electionConfigurationService;
            
            TextBoxPosition.Text=name;
            //TextBoxFaculty.Text = faculty;
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
              var value= _electionConfigurationService.AddPosition(new Position { PositionName = TextBoxPosition.Text });
                Id = value.Id;
            }
        }

        public PositionControl(ElectionConfigurationService electionConfigurationService)
        {
            InitializeComponent();
            _electionConfigurationService = electionConfigurationService;
            Loaded += PositionControl_Loaded;
            
        }

        private void PositionControl_Loaded(object sender, RoutedEventArgs e)
        {
            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click += BtnCancel_Click;
            _addPositionDialogControl.BtnSave.Click += BtnSave_Click;
            
            _contentDialog = new ContentDialog
            {
                Title = "Edit Position",
                Content = _addPositionDialogControl,
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Cancel"
            };
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            await _electionConfigurationService.UpdatePosition(new Position { Id = Id, PositionName =pos,Faculty = fac});
            _contentDialog.Hide();
        }

        private async void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _contentDialog.Hide();
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //todo show new values after save end edit
            //if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            //{
                
            //    //var value = ElectionService.GetPosition(_position);
            //    //set faculty text textbox from here
            //  await ElectionConfigurationService.UpdatePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
            //}

            //set TextBoxPosition IsEnabled = "true" after updating set TextBoxPosition IsEnabled = "False"

            _addPositionDialogControl.TextBoxPosition.Text = TextBoxPosition.Text;
            _addPositionDialogControl.TextBoxFaculty.Text = TextBoxFaculty.Text;
            
            await _contentDialog.ShowAsync();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs a)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                var response = System.Windows.MessageBox.Show("Are You Sure You Want to DELETE Position", "Delete",
                 System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning);

                if (response == System.Windows.MessageBoxResult.Yes)
                {
                    Admin.Administrators.AdminSetUpPositionPage.Instance.RemovePosition(this);
                  _electionConfigurationService.RemovePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
                }
            }
            
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;
using UniVoting.Services;
using Position = UniVoting.Core.Position;

namespace UniVoting.Admin.Administrators
{
    /// <summary>
    /// Interaction logic for PositionControl.xaml
    /// </summary>
    public partial class PositionControl : UserControl
    {
	    private IElectionConfigurationService _electionConfigurationService;

        /// <inheritdoc />
        public PositionControl()
	    {
            InitializeComponent();
	        Loaded += PositionControl_Loaded;
		    
	    }
        private CustomDialog _customDialog;
        private AddPositionDialogControl _addPositionDialogControl;
        private MetroWindow metroWindow;

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
            //TextBoxFaculty.Text = faculty;
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
              var value= _electionConfigurationService.AddPositionAsync(new Position { PositionName = TextBoxPosition.Text });
                Id = value.Id;
            }
        }

      

        private void PositionControl_Loaded(object sender, RoutedEventArgs e)
        {
            var bootStrapper = new BootStrapper();
            var container = bootStrapper.BootStrap();
            _electionConfigurationService = container.Resolve<IElectionConfigurationService>();
            _customDialog = new CustomDialog();
            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click += BtnCancel_Click;
            _addPositionDialogControl.BtnSave.Click += BtnSave_Click;
            _customDialog.Content = _addPositionDialogControl;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            var position =await _electionConfigurationService.GetPositionAsync(Id);
            position.PositionName = pos;
            position.Faculty = new Faculty{FacultyName = fac};
            await _electionConfigurationService.UpdatePositionAsync(position);
          await  metroWindow.HideMetroDialogAsync(_customDialog);
        }

        private async void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            await metroWindow.HideMetroDialogAsync(_customDialog);
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
             metroWindow = Window.GetWindow(this) as MetroWindow;
            var settings = new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Theme,
               AnimateShow = true,
            };
            await metroWindow.ShowMetroDialogAsync(_customDialog,settings);
            //todo show new values after save end edit
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {

                //var value = ElectionService.GetPositionAsync(_position);
                //set faculty text textbox from here
                await _electionConfigurationService.UpdatePositionAsync(new Position { Id = Id, PositionName = TextBoxPosition.Text });
            }

            //set TextBoxPosition IsEnabled = "true" after updating set TextBoxPosition IsEnabled = "False"

            _addPositionDialogControl.TextBoxPosition.Text = TextBoxPosition.Text;
            _addPositionDialogControl.TextBoxFaculty.Text = TextBoxFaculty.Text;

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs a)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                var response = MessageBox.Show("Are You Sure You Want to DELETE Position", "Delete",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (response == MessageBoxResult.Yes)
                {
                    AdminSetUpPositionPage.Instance.RemovePosition(this);
                  _electionConfigurationService.RemovePositionAsync(new Position { Id = Id, PositionName = TextBoxPosition.Text });
                }
            }
            
        }
    }
}

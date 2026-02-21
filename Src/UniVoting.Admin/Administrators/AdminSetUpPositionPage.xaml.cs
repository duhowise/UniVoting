using System.Windows;
using System.Windows.Controls;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpPositionPage : Page
    {
        public static AdminSetUpPositionPage Instance;
        private AddPositionDialogControl _addPositionDialogControl;
        private Window _dialogWindow;

        public AdminSetUpPositionPage()
        {
            InitializeComponent();
            Instance = this;
            Instance.Loaded += Instance_Loaded;
        }

        private async void Instance_Loaded(object sender, RoutedEventArgs e)
        {
            PositionControlHolder.Children.Clear();
            var positions = await ElectionConfigurationService.GetAllPositionsAsync();
            foreach (var position in positions)
                PositionControlHolder.Children.Add(new PositionControl
                {
                    TextBoxPosition = { Text = position.PositionName },
                    TextBoxFaculty = { Text = position.Faculty },
                    Id = position.Id
                });

            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click += BtnCancelClick;
            _addPositionDialogControl.BtnSave.Click += BtnSaveClick;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            _dialogWindow = new Window
            {
                Content = _addPositionDialogControl,
                Width = 650,
                Height = 220,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Window.GetWindow(this),
                WindowStyle = WindowStyle.ToolWindow,
                Title = "Add Position"
            };
            _dialogWindow.ShowDialog();
        }

        public void RemovePosition(UserControl c)
        {
            PositionControlHolder.Children.Remove(c);
        }

        private async void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            await ElectionConfigurationService.AddPosition(new Model.Position { PositionName = pos, Faculty = fac });
            PositionControlHolder.Children.Add(new PositionControl(pos));
            _dialogWindow?.Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }
    }
}

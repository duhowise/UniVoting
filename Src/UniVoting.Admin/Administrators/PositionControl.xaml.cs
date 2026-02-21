using System.Windows;
using System.Windows.Controls;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.Admin.Administrators
{
    public partial class PositionControl : UserControl
    {
        private AddPositionDialogControl _addPositionDialogControl;
        private Window _dialogWindow;

        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(PositionControl), new PropertyMetadata(0));

        public PositionControl(string name)
        {
            InitializeComponent();
            TextBoxPosition.Text = name;
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                var value = ElectionConfigurationService.AddPosition(new Position { PositionName = TextBoxPosition.Text });
                Id = value.Id;
            }
        }

        public PositionControl()
        {
            InitializeComponent();
            Loaded += PositionControl_Loaded;
        }

        private void PositionControl_Loaded(object sender, RoutedEventArgs e)
        {
            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click += BtnCancel_Click;
            _addPositionDialogControl.BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            await ElectionConfigurationService.UpdatePosition(new Position { Id = Id, PositionName = pos, Faculty = fac });
            _dialogWindow?.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _addPositionDialogControl.TextBoxPosition.Text = TextBoxPosition.Text;
            _addPositionDialogControl.TextBoxFaculty.Text = TextBoxFaculty.Text;
            _dialogWindow = new Window
            {
                Content = _addPositionDialogControl,
                Width = 650,
                Height = 220,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Window.GetWindow(this),
                WindowStyle = WindowStyle.ToolWindow,
                Title = "Edit Position"
            };
            _dialogWindow.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs a)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                var response = System.Windows.MessageBox.Show("Are You Sure You Want to DELETE Position", "Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (response == MessageBoxResult.Yes)
                {
                    Admin.Administrators.AdminSetUpPositionPage.Instance.RemovePosition(this);
                    ElectionConfigurationService.RemovePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
                }
            }
        }
    }
}

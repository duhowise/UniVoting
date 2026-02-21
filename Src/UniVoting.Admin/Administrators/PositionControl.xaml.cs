using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.Admin.Administrators
{
    public partial class PositionControl : UserControl
    {
        private AddPositionDialogControl _addPositionDialogControl = null!;
        private Window? _dialogWindow;

        public static readonly StyledProperty<int> IdProperty =
            AvaloniaProperty.Register<PositionControl, int>(nameof(Id));

        public int Id
        {
            get => GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

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

        private void PositionControl_Loaded(object? sender, RoutedEventArgs e)
        {
            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click += BtnCancel_Click;
            _addPositionDialogControl.BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object? sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            await ElectionConfigurationService.UpdatePosition(new Position { Id = Id, PositionName = pos, Faculty = fac });
            _dialogWindow?.Close();
        }

        private void BtnCancel_Click(object? sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }

        private void BtnEdit_Click(object? sender, RoutedEventArgs e)
        {
            _addPositionDialogControl.TextBoxPosition.Text = TextBoxPosition.Text;
            _addPositionDialogControl.TextBoxFaculty.Text = TextBoxFaculty.Text;
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _addPositionDialogControl,
                Width = 650,
                Height = 220,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Edit Position"
            };
            _dialogWindow.Show(owner);
        }

        private async void BtnDelete_Click(object? sender, RoutedEventArgs a)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxPosition.Text))
            {
                var response = await MessageBoxManager.GetMessageBoxStandard("Delete",
                    "Are You Sure You Want to DELETE Position", ButtonEnum.YesNo).ShowAsync();
                if (response == ButtonResult.Yes)
                {
                    AdminSetUpPositionPage.Instance?.RemovePosition(this);
                    ElectionConfigurationService.RemovePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
                }
            }
        }
    }
}

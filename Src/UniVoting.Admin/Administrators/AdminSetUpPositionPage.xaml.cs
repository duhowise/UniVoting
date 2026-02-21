using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpPositionPage : UserControl
    {
        public static AdminSetUpPositionPage? Instance;
        private AddPositionDialogControl _addPositionDialogControl = null!;
        private Window? _dialogWindow;
        private readonly IElectionConfigurationService _electionService;

        public AdminSetUpPositionPage()
        {
            InitializeComponent();
            _electionService = null!;
            Instance = this;
        }

        public AdminSetUpPositionPage(IElectionConfigurationService electionService)
        {
            _electionService = electionService;
            InitializeComponent();
            Instance = this;
            Loaded += Instance_Loaded;
        }

        private async void Instance_Loaded(object? sender, RoutedEventArgs e)
        {
            PositionControlHolder.Children.Clear();
            var positions = await _electionService.GetAllPositionsAsync();
            foreach (var position in positions)
            {
                var pc = new PositionControl(_electionService);
                pc.TextBoxPosition.Text = position.PositionName;
                pc.TextBoxFaculty.Text = position.Faculty;
                pc.Id = position.Id;
                PositionControlHolder.Children.Add(pc);
            }

            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click += BtnCancelClick;
            _addPositionDialogControl.BtnSave.Click += BtnSaveClick;
        }

        private void BtnAdd_Click(object? sender, RoutedEventArgs e)
        {
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _addPositionDialogControl,
                Width = 650,
                Height = 220,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Add Position"
            };
            _dialogWindow.Show(owner);
        }

        public void RemovePosition(UserControl c)
        {
            PositionControlHolder.Children.Remove(c);
        }

        private async void BtnSaveClick(object? sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            await _electionService.AddPosition(new Model.Position { PositionName = pos, Faculty = fac });
            PositionControlHolder.Children.Add(new PositionControl(pos, _electionService));
            _dialogWindow?.Close();
        }

        private void BtnCancelClick(object? sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }
    }
}

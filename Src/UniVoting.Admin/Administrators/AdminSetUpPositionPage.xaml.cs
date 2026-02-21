using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpPositionPage : UserControl
    {
        public static AdminSetUpPositionPage? Instance;
        private AddPositionDialogControl _addPositionDialogControl = null!;
        private Window? _dialogWindow;
        private readonly IElectionConfigurationService _electionService;
        private readonly IServiceProvider _sp;

        public AdminSetUpPositionPage()
        {
            InitializeComponent();
            _electionService = null!;
            _sp = null!;
            Instance = this;
        }

        public AdminSetUpPositionPage(IElectionConfigurationService electionService, IServiceProvider sp)
        {
            _electionService = electionService;
            _sp = sp;
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
                var pc = ActivatorUtilities.CreateInstance<PositionControl>(_sp, position.PositionName ?? string.Empty);
                pc.TextBoxFaculty.Text = position.Faculty;
                pc.Id = position.Id;
                PositionControlHolder.Children.Add(pc);
            }

            _addPositionDialogControl = _sp.GetRequiredService<AddPositionDialogControl>();
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
            PositionControlHolder.Children.Add(ActivatorUtilities.CreateInstance<PositionControl>(_sp, pos ?? string.Empty));
            _dialogWindow?.Close();
        }

        private void BtnCancelClick(object? sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }
    }
}

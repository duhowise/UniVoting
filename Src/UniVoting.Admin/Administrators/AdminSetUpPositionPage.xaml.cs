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

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminSetUpPositionPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
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
                var pc = _sp.GetRequiredService<PositionControl>();
                pc.TextBoxPosition.Text = position.PositionName ?? string.Empty;
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
            var newPc = _sp.GetRequiredService<PositionControl>();
            newPc.TextBoxPosition.Text = pos ?? string.Empty;
            PositionControlHolder.Children.Add(newPc);
            _dialogWindow?.Close();
        }

        private void BtnCancelClick(object? sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }
    }
}

using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IElectionConfigurationService _electionService;
        private readonly IServiceProvider _sp;

        public static readonly StyledProperty<int> IdProperty =
            AvaloniaProperty.Register<PositionControl, int>(nameof(Id));

        public int Id
        {
            get => GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public PositionControl()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public PositionControl(IElectionConfigurationService electionService, IServiceProvider sp)
        {
            _electionService = electionService;
            _sp = sp;
            InitializeComponent();
            Loaded += PositionControl_Loaded;
        }

        public PositionControl(string name, IElectionConfigurationService electionService, IServiceProvider sp)
        {
            _electionService = electionService;
            _sp = sp;
            InitializeComponent();
            TextBoxPosition.Text = name;
            Loaded += PositionControl_Loaded;
        }

        private void PositionControl_Loaded(object? sender, RoutedEventArgs e)
        {
            _addPositionDialogControl = _sp.GetRequiredService<AddPositionDialogControl>();
            _addPositionDialogControl.BtnCancel.Click += BtnCancel_Click;
            _addPositionDialogControl.BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object? sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
            await _electionService.UpdatePosition(new Position { Id = Id, PositionName = pos, Faculty = fac });
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
                    _electionService.RemovePosition(new Position { Id = Id, PositionName = TextBoxPosition.Text });
                }
            }
        }
    }
}

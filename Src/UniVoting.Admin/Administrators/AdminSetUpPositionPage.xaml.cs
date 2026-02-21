using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Admin.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpPositionPage : UserControl
    {
        public static AdminSetUpPositionPage? Instance;
        private AddPositionDialogControl _addPositionDialogControl = null!;
        private Window? _dialogWindow;
        private readonly AdminSetUpPositionPageViewModel _viewModel;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminSetUpPositionPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminSetUpPositionPage(IElectionConfigurationService electionService, IServiceProvider sp)
        {
            _sp = sp;
            _viewModel = new AdminSetUpPositionPageViewModel(electionService);
            _viewModel.ShowAddPositionDialog += ShowAddDialog;
            _viewModel.CloseDialog += () => _dialogWindow?.Close();
            DataContext = _viewModel;
            InitializeComponent();
            Instance = this;
            Loaded += async (_, _) =>
            {
                await _viewModel.LoadAsync();
                RefreshPositionControls();
                _viewModel.Positions.CollectionChanged += Positions_CollectionChanged;
            };
        }

        private void Positions_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                foreach (Position pos in e.NewItems)
                    AddPositionControl(pos);
            }
            else
            {
                RefreshPositionControls();
            }
        }

        private void RefreshPositionControls()
        {
            PositionControlHolder.Children.Clear();
            foreach (var position in _viewModel.Positions)
                AddPositionControl(position);
        }

        private void AddPositionControl(Position position)
        {
            var pc = _sp.GetRequiredService<PositionControl>();
            pc.TextBoxPosition.Text = position.PositionName ?? string.Empty;
            pc.TextBoxFaculty.Text = position.Faculty;
            pc.Id = position.Id;
            PositionControlHolder.Children.Add(pc);
        }

        private void ShowAddDialog()
        {
            _addPositionDialogControl = _sp.GetRequiredService<AddPositionDialogControl>();
            _addPositionDialogControl.BtnCancel.Click -= BtnCancelClick;
            _addPositionDialogControl.BtnSave.Click -= BtnSaveClick;
            _addPositionDialogControl.BtnCancel.Click += BtnCancelClick;
            _addPositionDialogControl.BtnSave.Click += BtnSaveClick;
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _addPositionDialogControl,
                Width = 650,
                Height = 220,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Add Position"
            };
            _dialogWindow.Show(owner!);
        }

        private async void BtnSaveClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _viewModel.NewPositionName = _addPositionDialogControl.TextBoxPosition.Text;
            _viewModel.NewFaculty = _addPositionDialogControl.TextBoxFaculty.Text;
            await _viewModel.SavePositionCommand.ExecuteAsync(null);
        }

        private void BtnCancelClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }

        public void RemovePosition(UserControl c)
        {
            PositionControlHolder.Children.Remove(c);
        }
    }
}

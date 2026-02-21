using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ExcelDataReader;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using UniVoting.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminAddVotersPage : UserControl
    {
        private readonly AdminAddVotersPageViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminAddVotersPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminAddVotersPage(IElectionConfigurationService electionService, IVotingService votingService)
        {
            _viewModel = new AdminAddVotersPageViewModel(electionService, votingService);
            _viewModel.ShowMessage += async (title, msg) =>
                await MessageBoxManager.GetMessageBoxStandard(title, msg).ShowAsync();
            DataContext = _viewModel;
            InitializeComponent();
            BtnSearch.Click += async (_, _) => await _viewModel.SearchCommand.ExecuteAsync(null);
            BtnImportFile.Click += BtnImportFile_Click;
            BtnSave.Click += BtnSave_Click;
            BtnReset.Click += async (_, _) => await _viewModel.ResetVoterCommand.ExecuteAsync(null);
            TextBoxIndexNumber.LostFocus += (_, _) => _viewModel.AddVoterFromFields();
        }

        private async void BtnSave_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_viewModel.HasPendingVoters)
            {
                var result = await MessageBoxManager.GetMessageBoxStandard("Save Voter List",
                    "Are You Sure You Want To Add this List Of Voters", ButtonEnum.YesNo).ShowAsync();
                if (result == ButtonResult.Yes)
                    await _viewModel.SaveCommand.ExecuteAsync(null);
            }
        }

        private async void BtnImportFile_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select Excel File",
                AllowMultiple = false,
                FileTypeFilter = new[] { new FilePickerFileType("Excel Files") { Patterns = new[] { "*.xls", "*.xlsx" } } }
            });
            if (files.Count == 0) return;
            try
            {
                var filePath = files[0].Path.LocalPath;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                });

                int indexName = 0, indexNumber = 0, faculty = 0;
                var importedVoters = new List<Voter>();

                foreach (DataTable table in dataSet.Tables)
                {
                    VoterGrid.ItemsSource = dataSet.Tables[table.TableName]?.DefaultView;
                    foreach (DataGridColumn col in VoterGrid.Columns)
                    {
                        var header = col.Header?.ToString()?.ToLower();
                        if (header == "fullname") indexName = col.DisplayIndex;
                        else if (header == "indexnumber") indexNumber = col.DisplayIndex;
                        else if (header == "faculty") faculty = col.DisplayIndex;
                    }
                }

                if (VoterGrid.ItemsSource is DataView dv)
                {
                    foreach (DataRowView row in dv)
                    {
                        try
                        {
                            importedVoters.Add(new Voter
                            {
                                VoterName = row[indexName]?.ToString() ?? string.Empty,
                                IndexNumber = row[indexNumber]?.ToString() ?? string.Empty,
                                Faculty = row[faculty]?.ToString() ?? string.Empty,
                                VoterCode = Util.GenerateRandomPassword(6)
                            });
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }
                }
                _viewModel.AddVotersFromData(importedVoters, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                await MessageBoxManager.GetMessageBoxStandard("File Read Error", ex.Message).ShowAsync();
            }
        }
    }
}

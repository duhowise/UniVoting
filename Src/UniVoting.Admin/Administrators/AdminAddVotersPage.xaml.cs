using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ExcelDataReader;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminAddVotersPage : UserControl
    {
        private DataSet _dataSet = null!;
        private int _indexName;
        private int _indexNUmber;
        private int _faculty;
        private int _added;
        private List<Voter> voters = null!;
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;

        public AdminAddVotersPage()
        {
            InitializeComponent();
            _electionService = null!;
            _votingService = null!;
            _dataSet = new DataSet();
            voters = new List<Voter>();
        }

        public AdminAddVotersPage(IElectionConfigurationService electionService, IVotingService votingService)
        {
            _electionService = electionService;
            _votingService = votingService;
            InitializeComponent();
            _dataSet = new DataSet();
            voters = new List<Voter>();
            BtnSearch.Click += BtnSearch_Click;
            BtnImportFile.Click += BtnImportFile_Click;
            BtnSave.Click += BtnSave_Click;
            TextBoxIndexNumber.LostFocus += TextBoxIndexNumber_LostFocus;
        }

        private async void BtnSearch_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Searchterm.Text))
            {
                var voter = await _votingService.GetVoterPass(new Voter { IndexNumber = Searchterm.Text });
                if (voter != null)
                {
                    await MessageBoxManager.GetMessageBoxStandard($"Name: {voter.VoterName}", $"Password: {voter.VoterCode}").ShowAsync();
                    Searchterm.Text = string.Empty;
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Password", $"Voter with Index Number: {Searchterm.Text} not found!").ShowAsync();
                    Searchterm.Text = string.Empty;
                }
            }
        }

        private void TextBoxIndexNumber_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) || !string.IsNullOrWhiteSpace(TextBoxIndexNumber.Text))
            {
                voters.Add(new Voter
                {
                    VoterName = TextBoxName.Text,
                    IndexNumber = TextBoxIndexNumber.Text,
                    VoterCode = Util.GenerateRandomPassword(6)
                });
            }
        }

        private async void BtnSave_Click(object? sender, RoutedEventArgs e)
        {
            if (voters.Count != 0)
            {
                var result = await MessageBoxManager.GetMessageBoxStandard("Save Voter List",
                    "Are You Sure You Want To Add this List Of Voters", ButtonEnum.YesNo).ShowAsync();
                if (result == ButtonResult.Yes)
                {
                    try
                    {
                        BtnSave.IsEnabled = false;
                        _added = await _electionService.AddVotersAsync(voters);
                        AddedCount.IsVisible = true;
                        AddedCount.Content = $"Added {_added} Voters";
                        voters.Clear();
                        _dataSet.Reset();
                        BtnSave.IsEnabled = true;
                    }
                    catch (Exception exception)
                    {
                        await MessageBoxManager.GetMessageBoxStandard("Voter Addition Error", exception.Message).ShowAsync();
                    }
                }
            }
        }

        private async void BtnImportFile_Click(object? sender, RoutedEventArgs e)
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
                var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                ImportedFilename.Content = "File Name " + Path.GetFileName(filePath);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    _dataSet = new DataSet();
                    _dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = data => new ExcelDataTableConfiguration { UseHeaderRow = true }
                    });
                    foreach (DataTable table in _dataSet.Tables)
                    {
                        VoterGrid.ItemsSource = _dataSet.Tables[table.TableName]?.DefaultView;
                    }
                    foreach (DataGridColumn column in VoterGrid.Columns)
                    {
                        if (column.Header?.ToString()?.ToLower() == "fullname") { _indexName = column.DisplayIndex; break; }
                    }
                    foreach (DataGridColumn column in VoterGrid.Columns)
                    {
                        if (column.Header?.ToString()?.ToLower() == "indexnumber") { _indexNUmber = column.DisplayIndex; break; }
                    }
                    foreach (DataGridColumn column in VoterGrid.Columns)
                    {
                        if (column.Header?.ToString()?.ToLower() == "faculty") { _faculty = column.DisplayIndex; break; }
                    }
                    if (VoterGrid.ItemsSource is System.Data.DataView dv)
                    {
                        foreach (DataRowView row in dv)
                        {
                            try
                            {
                                var voterInfo = new Voter
                                {
                                    VoterName = row[_indexName]?.ToString(),
                                    IndexNumber = row[_indexNUmber]?.ToString(),
                                    Faculty = row[_faculty]?.ToString(),
                                    VoterCode = Util.GenerateRandomPassword(6)
                                };
                                voters.Add(voterInfo);
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                await MessageBoxManager.GetMessageBoxStandard("File Read Error", exception.Message).ShowAsync();
            }
        }

        private async void BtnReset_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ResetIndexNumber.Text))
            {
                await _votingService.ResetVoter(new Voter { IndexNumber = ResetIndexNumber.Text });
                await MessageBoxManager.GetMessageBoxStandard("Success", "Successfully reset Voter").ShowAsync();
            }
        }
    }
}

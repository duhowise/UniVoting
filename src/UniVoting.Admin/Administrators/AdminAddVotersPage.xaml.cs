using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using UniVoting.Model;
using UniVoting.Services;
using MessageBox = System.Windows.MessageBox;

namespace UniVoting.Admin.Administrators
{
	/// <inheritdoc />
	/// <summary>
	/// Interaction logic for AdminAddVotersPage.xaml
	/// </summary>
	public partial class AdminAddVotersPage : Page
	{
		private DataSet _dataSet = null;
		private int _indexName;
		private int _indexNUmber;
		private int _added;
		private List<Voter> voters;
		private readonly VotingService _votingService;
		private readonly ElectionConfigurationService _electionConfigurationService;

		public AdminAddVotersPage(VotingService votingService, ElectionConfigurationService electionConfigurationService)
		{
			InitializeComponent();
			
			// Get services from DI container
			_votingService = votingService;
			_electionConfigurationService = electionConfigurationService;
			
			_dataSet=new DataSet();
			voters=new List<Voter>();
			BtnSearch.Click += BtnSearch_Click;
			BtnImportFile.Click += BtnImportFile_Click;
			BtnSave.Click += BtnSave_Click;
			TextBoxIndexNumber.LostFocus += TextBoxIndexNumber_LostFocus;
		  
		}

		private async void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Searchterm.Text))
			{
				var voter = await _votingService.GetVoterPass(new Voter { IndexNumber = Searchterm.Text });
				if (voter != null)
				{
					MessageBox.Show($"Name: {voter.VoterName}\nPassword: {voter.VoterCode}", "Voter Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
					Searchterm.Text = string.Empty;
				}
				else
				{
					MessageBox.Show($"Voter with Index Number: {Searchterm.Text} not found!", "Password", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
					Searchterm.Text = string.Empty;
				}
			}
		}
		
		private void TextBoxIndexNumber_LostFocus(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBoxName.Text)||!string.IsNullOrWhiteSpace(TextBoxIndexNumber.Text))
			{
				voters.Add(new Voter
				{
					VoterName = TextBoxName.Text,
					IndexNumber = TextBoxIndexNumber.Text,
					VoterCode = Util.GenerateRandomPassword(6)
				});
			}
		}

		private async void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (voters.Count != 0)
			{
				var dialogResult = MessageBox.Show("Are You Sure You Want To Add this List Of Voters", "Save Voter List", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
				if (dialogResult == System.Windows.MessageBoxResult.Yes)
				{
					try
					{
						BtnSave.IsEnabled = false;
						_added = await _electionConfigurationService.AddVotersAsync(voters);
						AddedCount.Visibility = Visibility.Visible;
						AddedCount.Content = $"Added {_added} Voters";
						BtnSave.IsEnabled = true;
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message, "Voter Addition Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
					}
				}
			}
		}

		private void BtnImportFile_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog() { Filter = @"Excel 1996-2007 Files |*.xls;*.xlsx;", ValidateNames = true })
			{
			    if (openFileDialog.ShowDialog() != DialogResult.OK) return;
			    try
			    {
			        voters.Clear();
                    var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
			        ImportedFilename.Content = "File Name" + " " + openFileDialog.SafeFileName;
					    
			        using (var reader = ExcelReaderFactory.CreateReader(stream))
			        {
			            _dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
			            {
			                ConfigureDataTable = data => new ExcelDataTableConfiguration
			                {
			                    UseHeaderRow = true
			                }
			            });

			            foreach (DataTable table in _dataSet.Tables)
			            {
			                VoterGrid.ItemsSource = _dataSet.Tables[table.TableName].DefaultView;
			            }
			            
			            foreach (DataGridColumn column in VoterGrid.Columns)
			            {
			                if (column.Header.ToString().ToLower() != "fullname") continue;
			                _indexName = column.DisplayIndex;
			                break;
			            }
			            
			            foreach (var column in VoterGrid.Columns)
			            {
			                if (column.Header.ToString().ToLower() != "indexnumber") continue;
			                _indexNUmber = column.DisplayIndex;
			                break;
			            }
			            
                        foreach (DataRowView row in VoterGrid.ItemsSource)
                        {
                            var voterInfo = new Voter
                            {
                                VoterName = row[_indexName].ToString(),
                                IndexNumber = row[_indexNUmber].ToString(),
                                VoterCode = Util.GenerateRandomPassword(6)
                            };
                            voters.Add(voterInfo);
                        }
                    }
                }
			    catch (Exception exception)
			    {
			        MessageBox.Show(exception.Message, "File Read Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
			    }
			}
		}

		private async void BtnReset_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(ResetIndexNumber.Text))
			{
				await _votingService.ResetVoter(new Voter { IndexNumber = ResetIndexNumber.Text });
			}
		}
	}
}

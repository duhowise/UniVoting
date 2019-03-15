using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using ExcelDataReader;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Core;
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
		private readonly IElectionConfigurationService _electionConfigurationService;
	    private readonly IVotingService _votingService;
	    private DataSet _dataSet = null;
		private int _indexName;
		private int _indexNUmber;
		private int _faculty;
		private int _added;
		private List<Voter> voters;
	    readonly MetroWindow metroWindow;

        public AdminAddVotersPage(IElectionConfigurationService electionConfigurationService,IVotingService votingService)
		{
			_electionConfigurationService = electionConfigurationService;
		    _votingService = votingService;
		    InitializeComponent();
			_dataSet=new DataSet();
            metroWindow = (Window.GetWindow(this) as MetroWindow);
            voters =new List<Voter>();
			BtnSearch.Click += BtnSearch_Click;
			BtnImportFile.Click += BtnImportFile_Click;
			BtnSave.Click += BtnSave_Click;
			TextBoxIndexNumber.LostFocus += TextBoxIndexNumber_LostFocus;
		  
		}

		private async void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Searchterm.Text))
			{
				var voter=	await _votingService .GetVoterPass(Searchterm.Text);
				if (voter!=null)
				{

					
					var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 20, AffirmativeButtonText = "Ok" };
					await metroWindow.ShowMessageAsync($"Name:{voter.VoterName}", $"Password: {voter.VoterCode}", MessageDialogStyle.Affirmative, dialogSettings);
					Searchterm.Text = string.Empty;
				}
				else
				{
					await metroWindow.ShowMessageAsync("Password",$"Voter with Index Number: {Searchterm.Text} not found!");
					Searchterm.Text=string.Empty;
					
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
			if (voters.Count!=0)
			{
				var window = Window.GetWindow(this) as MetroWindow;
				var dialogResult = await window.ShowMessageAsync("Save Voter List", "Are You Sure You Want To Add this List Of Voters", MessageDialogStyle.AffirmativeAndNegative);
				if (dialogResult == MessageDialogResult.Affirmative)
				{
					
					try
					{
						BtnSave.IsEnabled = false;
						_added =  _electionConfigurationService.AddVotersAsync(voters).Result.Count;
						AddedCount.Visibility=Visibility.Visible;
						AddedCount.Content = $"Added {_added} Voters";
                        voters.Clear();
                        _dataSet.Reset();
						BtnSave.IsEnabled = true;

                    }
                    catch (Exception exception)
					{
					await	metroWindow.ShowMessageAsync("Voter Addition Error", exception.Message);
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
			        var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
			        ImportedFilename.Content ="File Name"+" "+ openFileDialog.SafeFileName;
					    

			        using (var reader = ExcelReaderFactory.CreateReader(stream))
			        {
                        _dataSet=new DataSet();
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
			            foreach (var column in VoterGrid.Columns)
			            {
			                if (column.Header.ToString().ToLower() != "faculty") continue;
			                _faculty = column.DisplayIndex;
			                break;
			            }
                        foreach (DataRowView row in VoterGrid.ItemsSource)
                        {
                            try
                            {
                                var voterInfo = new Voter();
                                voterInfo.VoterName = row[_indexName].ToString();
                                voterInfo.IndexNumber = row[_indexNUmber].ToString();
                                voterInfo.Faculty = new Faculty
                                {
                                    FacultyName = row[_faculty].ToString()
                                };
                                voterInfo.VoterCode = Util.GenerateRandomPassword(6);
                                voters.Add(voterInfo);
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception);
                            }
                           
                    
                        }


                    }



                }
			    catch (Exception exception)
			    {
			        MessageBox.Show(exception.Message, "File Read Error");
			    }
			}
		}

		private async void BtnReset_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(ResetIndexNumber.Text))
			{
				await _votingService.ResetVoter(new Voter { IndexNumber = ResetIndexNumber.Text });
			    await metroWindow.ShowMessageAsync("Sucecss", $"Successfully reset Voter");

            }
        }
	}
}

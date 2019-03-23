using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Autofac;
using ExcelDataReader;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;
using UniVoting.Services;
using MessageBox = System.Windows.MessageBox;

namespace UniVoting.Admin.Administrators
{
	/// <inheritdoc />
	/// <summary>
	/// Interaction logic for AdminAddVotersPage.xaml
	/// </summary>
	public partial class AdminAddVotersPage : MetroWindow
	{
		private readonly IElectionConfigurationService _electionConfigurationService;
	    private readonly IVotingService _votingService;
	    private DataSet _dataSet = null;
		private int _indexName;
		private int _indexNUmber;
		private int _faculty;
		private List<Voter> voters;
        private IContainer _container;

        public AdminAddVotersPage()
        {
            _container = new BootStrapper().BootStrap();
			_electionConfigurationService = _container.Resolve<IElectionConfigurationService>();
		    _votingService = _container.Resolve<IVotingService>();
		    InitializeComponent();
			_dataSet=new DataSet();
            voters =new List<Voter>();
			BtnSearch.Click += BtnSearch_Click;
			BtnImportFile.Click += BtnImportFile_Click;
			BtnSave.Click += BtnSave_Click;
			TextBoxIndexNumber.LostFocus += TextBoxIndexNumber_LostFocus;
            Loaded += AdminAddVotersPage_Loaded;
		  
		}

        private async void AdminAddVotersPage_Loaded(object sender, RoutedEventArgs e)
        {
            FacultyList.ItemsSource =await _electionConfigurationService.GetFacultiesAsync();
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Searchterm.Text))
			{
				var voter=	await _votingService .GetVoterPass(Searchterm.Text);
				if (voter!=null)
				{

					
					var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 20, AffirmativeButtonText = "Ok" };
					await this.ShowMessageAsync($"Name:{voter.VoterName}", $"Password: {voter.VoterCode}", MessageDialogStyle.Affirmative, dialogSettings);
					Searchterm.Text = string.Empty;
				}
				else
				{
					await this.ShowMessageAsync("Password",$"Voter with Index Number: {Searchterm.Text} not found!");
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
					VoterCode = Util.GenerateRandomPassword(6),
                    FacultyId = Convert.ToInt32(FacultyList.SelectedValue)
				});
			}
		}

		private async void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (voters.Count!=0)
			{
				var dialogResult = await this.ShowMessageAsync("Save Voter List", "Are You Sure You Want To Add this List Of Voters", MessageDialogStyle.AffirmativeAndNegative);
				if (dialogResult == MessageDialogResult.Affirmative)
				{
					
					try
					{
						BtnSave.IsEnabled = false;
                        var data =await _electionConfigurationService.AddVotersAsync(voters);
						AddedCount.Visibility=Visibility.Visible;
						AddedCount.Content = $"Added {data.Count} Voters";
                        voters.Clear();
                        _dataSet.Reset();
						BtnSave.IsEnabled = true;

                    }
                    catch (Exception exception)
					{
					await	this.ShowMessageAsync("Voter Addition Error", exception.Message);
					}

				}
			}
		}

		private async void BtnImportFile_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog() { Filter = @"Excel 1996-2007 Files |*.xls;*.xlsx;", ValidateNames = true })
			{
			    if (openFileDialog.ShowDialog()!=System.Windows.Forms.DialogResult.OK) return;
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
			                if (column.Header.ToString().ToLower() != "facultyId") continue;
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
                    await this.ShowMessageAsync("Error", $"File read Error\n {exception}");

                }
            }
		}

		private async void BtnReset_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(ResetIndexNumber.Text))
			{
				await _votingService.ResetVoter(new Voter { IndexNumber = ResetIndexNumber.Text });
			    await this.ShowMessageAsync("Sucecss", $"Successfully reset Voter");

            }
        }
	}
}

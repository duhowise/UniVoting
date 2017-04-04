using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Excel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for AdminAddVotersPage.xaml
    /// </summary>
    public partial class AdminAddVotersPage : Page
    {
        private DataSet _dataSet = null;
        private int indexName;
        private int indexNUmber;
        private int added;
        private List<Voter> voters;
        public AdminAddVotersPage()
        {
            InitializeComponent();
            _dataSet=new DataSet();
            voters=new List<Voter>();
            BtnImportFile.Click += BtnImportFile_Click;
            BtnSave.Click += BtnSave_Click;
            TextBoxIndexNumber.LostFocus += TextBoxIndexNumber_LostFocus;
        }

        private void TextBoxIndexNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text)||string.IsNullOrWhiteSpace(TextBoxIndexNumber.Text))
            {
                voters.Add(new Voter
                {
                    VoterName = TextBoxName.Text,
                    IndexNumber = TextBoxIndexNumber.Text
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
                    added = ElectionConfigurationService.AddVoters(voters);

                }
            }
        }

        private void BtnImportFile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = @"Excel 2007 Files | *.xls;", ValidateNames = true })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    ImportedFilename.Content =ImportedFilename.Content+" "+ openFileDialog.SafeFileName;
                    IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fs);
                    reader.IsFirstRowAsColumnNames = true;
                    _dataSet = reader.AsDataSet();
                    foreach (DataTable table in _dataSet.Tables)
                    {

                        VoterGrid.ItemsSource = _dataSet.Tables[table.TableName].DefaultView;

                    }
                    foreach (DataGridColumn column in VoterGrid.Columns)
                    {
                        if (column.Header.ToString().ToLower() == "fullname")
                        {
                            indexName = column.DisplayIndex;
                            break;
                        }
                        
                    }
                    foreach (DataGridColumn column in VoterGrid.Columns)
                    {
                        if (column.Header.ToString().ToLower() == "indexnumber")
                        {
                            indexNUmber = column.DisplayIndex;
                            break;
                        }
                    }
                    foreach (System.Data.DataRowView row in VoterGrid.ItemsSource)
                    {
                        var data = new Voter
                        {
                            VoterName = row[indexName].ToString(),
                            IndexNumber = row[indexNUmber].ToString(),
                            VoterCode = Util.GenerateRandomPassword(6)
                          
                        };
                        voters.Add(data);
                    }

                }
            }
        }
    }
}

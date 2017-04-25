using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Media;
using Akavache;
using MahApps.Metro;
using UniVoting.Model;
using UniVoting.Services;
using Settings = UniVoting.Client.Properties.Settings;

namespace UniVoting.Client
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IEnumerable<Position> _positions;

		protected override async void OnStartup(StartupEventArgs e)
		{
			Setting data=new Setting();

			BlobCache.ApplicationName = "VotingApplication";
			//if (Settings.Default.FirstRun)
			//{
				_positions = new List<Position>();
				

				try
				{
					var	electionData = await ElectionConfigurationService.ConfigureElection(Convert.ToInt32(Settings.Default.ElectionId));
					//List<string> color = new List<string>();
					//color.AddRange(electionData.Colour.Split(',')); 
					_positions = ElectionConfigurationService.GetAllPositions();
					await BlobCache.LocalMachine.InsertObject("ElectionSettings", electionData);
					await BlobCache.LocalMachine.InsertObject("ElectionPositions", _positions);

				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message, " Election Settings Error");
				}
			   
				Settings.Default.FirstRun = false;
				Settings.Default.Save();
			//}



			//get color from  local cache
			data =await BlobCache.LocalMachine.GetObject<Setting>("ElectionSettings");
			var rgb = data.Colour.Split(',');
			// add custom accent and theme resource dictionaries
			//ThemeManager.AddAccent("CustomAccent1",
			//    new Uri("pack://application:,,,/UniVoting.Client;component/CustomAccents/CustomAccent.xaml"));

			// create custom accents


			
			ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
			ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
			ThemeManagerHelper.CreateAppStyleBy(new Color {R = Convert.ToByte(rgb[0]), G = Convert.ToByte(rgb[1]), B = Convert.ToByte(rgb[2]) }, true);
			MainWindow = new ClientsLoginWindow();
			MainWindow.Show();
			base.OnStartup(e);
		}
	}
}

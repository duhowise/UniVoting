﻿using System.ComponentModel;
using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.WPF.Administrators;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniVoting.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{

        public MainWindow(Comissioner comissioner)
		{
			InitializeComponent();
           
			
		   PageHolder.Content = new AdminMenuPage(comissioner);

		}

      

        protected override void OnClosing(CancelEventArgs e)
		{
			new AdminLoginWindow().Show();
			base.OnClosing(e);
		}


    }
}

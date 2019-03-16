﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for AdminDispensePasswordWindow.xaml
	/// </summary>
	public partial class AdminDispensePasswordWindow : MetroWindow
	{
		private readonly IElectionConfigurationService _electionConfigurationService;
		private List<Voter> voters;
		public AdminDispensePasswordWindow()
		{
		    var container = new BootStrapper().BootStrap();
		    _electionConfigurationService = container.Resolve<IElectionConfigurationService>();
            InitializeComponent();
			Loaded += AdminDispensePasswordWindow_Loaded;
			StudentName.TextChanged += StudentName_TextChanged;
			StudentsSearchList.MouseDoubleClick += StudentsSearchList_MouseDoubleClick;
			RefreshList.Click += RefreshList_Click;
		}

		private void RefreshList_Click(object sender, RoutedEventArgs e)
		{
			RefreshStudentList();
		}

		private async void StudentsSearchList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var metroWindow = (GetWindow(this) as MetroWindow);
			var student = StudentsSearchList.SelectedItem as Voter;
			if (student!=null)
			{
				var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 20, AffirmativeButtonText = "Ok" };
				await metroWindow.ShowMessageAsync($"Name:{student.VoterName}", $"Password: {student.VoterCode}", MessageDialogStyle.Affirmative, dialogSettings);
				StudentName.Text = String.Empty;
				StudentName.Focus();
			}
		}

		private void StudentName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(StudentName.Text))
			{
			StudentsSearchList.ItemsSource = voters.FindAll(x => x.VoterName.ToLower().StartsWith(StudentName.Text.ToLower())
			||x.IndexNumber.ToLower().StartsWith(StudentName.Text.ToLower()));
			}
		}

		private void AdminDispensePasswordWindow_Loaded(object sender, RoutedEventArgs e)
		{
			StudentName.Focus();
			RefreshStudentList();
		}

		private async void RefreshStudentList()
		{
			voters = new List<Voter>();
		voters=(List<Voter>) await	_electionConfigurationService.GetAllVotersAsync();
			StudentsSearchList.ItemsSource = voters;
		}
	}
}

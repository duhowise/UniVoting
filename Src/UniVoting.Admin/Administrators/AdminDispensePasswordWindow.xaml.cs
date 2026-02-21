using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminDispensePasswordWindow : Window
    {
        private List<Voter> voters = new List<Voter>();

        public AdminDispensePasswordWindow()
        {
            InitializeComponent();
            Loaded += AdminDispensePasswordWindow_Loaded;
            StudentName.TextChanged += StudentName_TextChanged;
            StudentsSearchList.DoubleTapped += StudentsSearchList_DoubleTapped;
            RefreshList.Click += RefreshList_Click;
        }

        private void RefreshList_Click(object? sender, RoutedEventArgs e)
        {
            RefreshStudentList();
        }

        private async void StudentsSearchList_DoubleTapped(object? sender, TappedEventArgs e)
        {
            var student = StudentsSearchList.SelectedItem as Voter;
            if (student != null)
            {
                await MessageBoxManager.GetMessageBoxStandard($"Name: {student.VoterName}", $"Password: {student.VoterCode}").ShowAsync();
                StudentName.Text = string.Empty;
                StudentName.Focus();
            }
        }

        private void StudentName_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StudentName.Text))
            {
                StudentsSearchList.ItemsSource = voters.FindAll(x =>
                    (x.VoterName?.ToLower().StartsWith(StudentName.Text.ToLower()) ?? false) ||
                    (x.IndexNumber?.ToLower().StartsWith(StudentName.Text.ToLower()) ?? false));
            }
        }

        private void AdminDispensePasswordWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            StudentName.Focus();
            RefreshStudentList();
        }

        private async void RefreshStudentList()
        {
            voters = new List<Voter>();
            voters = (List<Voter>)await ElectionConfigurationService.GetAllVotersAsync();
            StudentsSearchList.ItemsSource = voters;
        }
    }
}
